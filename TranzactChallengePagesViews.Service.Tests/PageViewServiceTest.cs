using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core.Model;
using TranzactChallengePagesViews.Service.Interfaces;
using Xunit;

namespace TranzactChallengePagesViews.Service.Tests
{
    public class PageViewServiceTest
    {
        private readonly Mock<ILinkService> _mockLinkService;
        private readonly Mock<IDownloadUtil> _mockDownloadUtil;
        private readonly Mock<IStreamUtil> _mockStreamUtil;
        private readonly Mock<IPrintService> _mockPrintService;
        private readonly PageViewService _pageViewService;

        public PageViewServiceTest()
        {
            _mockLinkService = new Mock<ILinkService>();
            _mockDownloadUtil = new Mock<IDownloadUtil>();
            _mockStreamUtil = new Mock<IStreamUtil>();
            _mockPrintService = new Mock<IPrintService>();
            _pageViewService = new PageViewService(_mockLinkService.Object, _mockDownloadUtil.Object, _mockStreamUtil.Object, _mockPrintService.Object);
        }

        [Fact]
        public void ConvertToListPageView_ShouldReturnOnePageViewInList()
        {
            List<string> lines = new List<string>();
            lines.Add("aa - 1 0");
            lines.Add("");

            List<PageView> result = _pageViewService.ConvertToListPageView(lines);
            List<PageView> resultToMatch = new List<PageView>();

            resultToMatch.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "-",
                CountViews = 1
            });

            Assert.Equal(resultToMatch.Count, result.Count);
            Assert.Equal(resultToMatch[0].DomainCode, result[0].DomainCode);
            Assert.Equal(resultToMatch[0].PageTitle, result[0].PageTitle);
            Assert.Equal(resultToMatch[0].CountViews, result[0].CountViews);
        }

        [Fact]
        public void GetTopPageViews_ShouldReturnTopTwoPageViews()
        {
            List<PageView> pageViews = new List<PageView>();
            pageViews.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "-",
                CountViews = 1
            });
            pageViews.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "-",
                CountViews = 1
            });
            pageViews.Add(new PageView()
            {
                DomainCode = "bb",
                PageTitle = "wiki",
                CountViews = 4
            });
            pageViews.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "-",
                CountViews = 2
            });
            pageViews.Add(new PageView()
            {
                DomainCode = "pp",
                PageTitle = "-",
                CountViews = 6
            });
            pageViews.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "-",
                CountViews = 1
            });
            int top = 2;
            List<PageView> result = _pageViewService.GetTopPageViews(pageViews, top);
            List<PageView> resultToMatch = new List<PageView>();

            resultToMatch.Add(new PageView()
            {
                DomainCode = "pp",
                PageTitle = "-",
                CountViews = 6
            });
            resultToMatch.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "-",
                CountViews = 5
            });


            Assert.Equal(resultToMatch.Count, result.Count);
            Assert.Equal(resultToMatch[0].DomainCode, result[0].DomainCode);
            Assert.Equal(resultToMatch[0].DomainCode, result[0].DomainCode);
            Assert.Equal(resultToMatch[0].PageTitle, result[0].PageTitle);
            Assert.Equal(resultToMatch[1].PageTitle, result[1].PageTitle);
            Assert.Equal(resultToMatch[1].CountViews, result[1].CountViews);
            Assert.Equal(resultToMatch[1].CountViews, result[1].CountViews);
        }

        [Fact]
        public async Task GetTopViewsByDomainAndPage_ShouldReturnTopTwoPages()
        {
            int linksHoursLimit = 1;
            int topPages = 2;
            DateTime utcNow = DateTime.UtcNow;
            List<string> linksToReturn = new List<string>();
            linksToReturn.Add("https://dumps.wikimedia.org/other/pageviews/2021/2021-05/pageviews-20210509-090000.gz");
            _mockLinkService
                .Setup(x => x.GetLinksFromLastHours(linksHoursLimit, utcNow))
                .Returns(linksToReturn);

            Stream stream = new MemoryStream();
            _mockDownloadUtil
                .Setup(x => x.GetPageViewStream(linksToReturn[0]))
                .Returns(Task.FromResult(stream));

            MemoryStream memoryStream = new MemoryStream();
            _mockStreamUtil
                .Setup(x => x.DecompressFromStream(stream))
                .Returns(memoryStream);

            List<string> lines = new List<string>();
            lines.Add("aa - 1 0");
            lines.Add("aa - 2 0");
            lines.Add("aa page 2 0");
            _mockStreamUtil
                .Setup(x => x.ReadLinesFromStream(memoryStream, Encoding.UTF8))
                .Returns(Task.FromResult(lines));

            _mockPrintService.Setup(x => x.PrintResults(new List<PageView>()));

            List<PageView> result = await _pageViewService.GetTopViewsByDomainAndPage(linksHoursLimit, utcNow, topPages);
            List<PageView> resultToMatch = new List<PageView>();

            resultToMatch.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "-",
                CountViews = 3
            });
            resultToMatch.Add(new PageView()
            {
                DomainCode = "aa",
                PageTitle = "page",
                CountViews = 2
            });

            Assert.Equal(resultToMatch.Count, result.Count);
            Assert.Equal(resultToMatch[0].DomainCode, result[0].DomainCode);
            Assert.Equal(resultToMatch[0].PageTitle, result[0].PageTitle);
            Assert.Equal(resultToMatch[0].CountViews, result[0].CountViews);
            Assert.Equal(resultToMatch[1].DomainCode, result[1].DomainCode);
            Assert.Equal(resultToMatch[1].PageTitle, result[1].PageTitle);
            Assert.Equal(resultToMatch[1].CountViews, result[1].CountViews);
        }

    }
}
