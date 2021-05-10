using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranzactChallengePagesViews.Service.Interfaces
{
    public interface ILinkService
    {
        List<string> GetLinksFromLastHours(int hoursLimit, DateTime dateToStart);
        string GetLinkFromDate(DateTime dateTime);
    }
}
