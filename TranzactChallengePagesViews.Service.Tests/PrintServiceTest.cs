using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core.Model;
using TranzactChallengePagesViews.Service.Interfaces;
using Xunit;

namespace TranzactChallengePagesViews.Service.Tests
{
    public class PrintServiceTest
    {
        private readonly IPrintService _printService;

        public PrintServiceTest()
        {
            _printService = new PrintService();
        }

        [Fact]
        public void GetColumnFromValue_ShouldReturnStringValueColumInsideLimit()
        {
            int limit = 6;
            string value = "page";
            string result = _printService.GetColumnFromValue(value, limit);
            string resultToMatch = "page  ";

            Assert.Equal(result, resultToMatch);
        }

        [Fact]
        public void GetColumnFromValue_ShouldReturnStringValueColumAboveLimit()
        {
            int limit = 6;
            string value = "pageTitle";
            string result = _printService.GetColumnFromValue(value, limit);
            string resultToMatch = "pageTi";

            Assert.Equal(resultToMatch, result);
        }

        [Fact]
        public void GetColumnFromValue_ShouldReturnStringValueColumnEmpty()
        {
            int limit = 6;
            string value = null;
            string result = _printService.GetColumnFromValue(value, limit);
            string resultToMatch = "      ";

            Assert.Equal(resultToMatch, result);
        }

        [Fact]
        public void GetRowToPrint_ShouldReturnRowFromPageView()
        {
            PageView pageView = new PageView()
            {
                DomainCode = "domain",
                PageTitle = "pageTitle",
                CountViews = 1
            };
            string result = _printService.GetRowToPrint(pageView);
            string resultToMatch = "domain          pageTitle                1               ";

            Assert.Equal(resultToMatch, result);
        }
    }
}
