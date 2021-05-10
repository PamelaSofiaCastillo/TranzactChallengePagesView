using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core;
using TranzactChallengePagesViews.Service.Interfaces;

namespace TranzactChallengePagesViews
{
    public class Main
    {
        private readonly IPageviewService _pageViewService;
        public Main(IPageviewService pageViewService)
        {
            _pageViewService = pageViewService;
        }

        public void Run()
        {
            Task.WaitAll(_pageViewService.GetTopViewsByDomainAndPage(Constants.LastHours, DateTime.UtcNow, Constants.TopPageViews));
        }
    }
}
