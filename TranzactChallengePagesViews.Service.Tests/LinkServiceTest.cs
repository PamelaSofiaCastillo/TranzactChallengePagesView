using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core;
using TranzactChallengePagesViews.Service;
using Xunit;

namespace TranzactChallengePagesViews.Service.Tests
{
    public class LinkServiceTest
    {
        private readonly IConfiguration _configuration;
        private readonly LinkService _linkService;
        private readonly string baseUrl = "https://dumps.wikimedia.org/other/pageviews";
        public LinkServiceTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"BaseDownloadUrlPath", baseUrl},
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _linkService = new LinkService(_configuration);
        }

        [Fact]
        public void GetLinkFromDate_ShouldReturnLinkFromDate()
        {
            // May 9 20201 10 am
            DateTime dateTime = new DateTime(2021, 5, 9, 10, 0, 0);
            string baseUrl = "https://dumps.wikimedia.org/other/pageviews";

            string result = _linkService.GetLinkFromDate(dateTime);
            string resultToMatch = $"{baseUrl}/2021/2021-05/pageviews-20210509-100000.gz";

            Assert.Equal(resultToMatch, result);
        }
        
        [Fact]
        public void GetLinksFromLastHours_ShouldReturnLinksFromLastHours()
        {
            // May 9 20201 10 am
            DateTime dateTime = new DateTime(2021, 5, 9, 10, 0, 0);
            int hoursLimit = 1;
            List<string> result = _linkService.GetLinksFromLastHours(hoursLimit, dateTime);
            List<string> resultToMatch = new List<string>();
            
            resultToMatch.Add($"{baseUrl}/2021/2021-05/pageviews-20210509-090000.gz");

            Assert.Equal(resultToMatch.Count, result.Count);
            Assert.Equal(resultToMatch[0], result[0]);
        }
    }
}
