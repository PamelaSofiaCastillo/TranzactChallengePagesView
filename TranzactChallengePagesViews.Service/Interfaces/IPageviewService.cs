using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core.Model;

namespace TranzactChallengePagesViews.Service.Interfaces
{
    public interface IPageviewService
    {
        Task<List<PageView>> GetTopViewsByDomainAndPage(int lastHours, DateTime dateToTake, int top);
        List<PageView> ConvertToListPageView(List<string> lines);

        List<PageView> GetTopPageViews(List<PageView> pageViews, int topPageViews);
    }
}
