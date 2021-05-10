using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core;
using TranzactChallengePagesViews.Service.Interfaces;

namespace TranzactChallengePagesViews.Service
{
    public class LinkService : ILinkService
    {
        private readonly IConfiguration _configuration;

        public LinkService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<string> GetLinksFromLastHours(int hoursLimit, DateTime dateToStart)
        {
            List<string> linksToDownload = new List<string>();

            for (int i = 1; i <= hoursLimit; i++)
            {
                DateTime newDateSubtracted = dateToStart.AddHours(i * -1);
                string linkFromDate = GetLinkFromDate(newDateSubtracted);
                linksToDownload.Add(linkFromDate);
            }

            return linksToDownload;
        }

        public string GetLinkFromDate(DateTime dateTime)
        {
            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString("00");
            string day = dateTime.Day.ToString("00");
            string hour = dateTime.Hour.ToString("00");

            string baseUrl = _configuration.GetValue<string>(Constants.BasePathName);

            return $"{baseUrl}/{year}/{year}-{month}/pageviews-{year}{month}{day}-{hour}0000.gz";
        }
    }
}
