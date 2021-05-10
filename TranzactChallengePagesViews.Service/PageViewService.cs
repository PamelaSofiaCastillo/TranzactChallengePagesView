using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core;
using TranzactChallengePagesViews.Core.Model;
using TranzactChallengePagesViews.Service.Interfaces;
using TranzactChallengePagesViews.Utils;

namespace TranzactChallengePagesViews.Service
{
    public class PageViewService : IPageviewService
    {
        private readonly ILinkService _linkService;
        private readonly IDownloadUtil _downloadUtil;
        private readonly IStreamUtil _streamUtil;
        private readonly IPrintService _printService;
        public PageViewService(ILinkService linkService, IDownloadUtil downloadUtil, IStreamUtil streamUtil, IPrintService printService)
        {
            _linkService = linkService;
            _downloadUtil = downloadUtil;
            _streamUtil = streamUtil;
            _printService = printService;
        }

        public async Task<List<PageView>> GetTopViewsByDomainAndPage(int lastHours, DateTime dateToTake, int top)
        {
            try
            {
                List<string> linksFromLastHours = _linkService.GetLinksFromLastHours(lastHours, dateToTake);
                List<PageView> pageViews = new List<PageView>();

                foreach (string link in linksFromLastHours)
                {
                    Console.WriteLine("Downloading from link: {0} ...", link);
                    Stream streamFile = await _downloadUtil.GetPageViewStream(link);
                    Console.WriteLine("Downloaded link: {0}", link);

                    MemoryStream memoryStream = _streamUtil.DecompressFromStream(streamFile);

                    List<string> lines = await _streamUtil.ReadLinesFromStream(memoryStream, Encoding.UTF8);
                    List<PageView> pageViewsLines = ConvertToListPageView(lines);
                    pageViews.AddRange(pageViewsLines);
                }

                Console.WriteLine("Downloaded All files");

                Console.WriteLine("Obtaining Top {0} PageViews", top);

                List<PageView> topPageViews = GetTopPageViews(pageViews, top);
                _printService.PrintResults(topPageViews);

                return topPageViews;
            }
            catch (Exception error)
            {
                Console.WriteLine("Ocurrió un error al obtener el top de PageViews: {0}", error.Message);
                return null;
            }
        }


        public List<PageView> ConvertToListPageView(List<string> lines)
        {
            return lines.Select(line =>
            {
                string[] ss = line.Split(" ", 4);
                return (ss.Length == 4)
                       ? new PageView() { DomainCode = ss[0], PageTitle = ss[1], CountViews = int.Parse(ss[2]) }
                       : null;
            })
            .Where(x => x != null)
            .ToList();
        }

        public List<PageView> GetTopPageViews(List<PageView> pageViews, int topPageViews)
        {
            return pageViews
                .GroupBy(x => new { x.DomainCode, x.PageTitle })
                .Select(y => new PageView
                {
                    DomainCode = y.First().DomainCode,
                    PageTitle = y.First().PageTitle,
                    CountViews = y.Sum(z => z.CountViews),
                })
                .OrderByDescending(x => x.CountViews)
                .Take(topPageViews).ToList();
        }
    }
}
