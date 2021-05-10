using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core.Model;

namespace TranzactChallengePagesViews.Service.Interfaces
{
    public interface IPrintService
    {
        void PrintResults(List<PageView> pageViews);

        string GetRowToPrint(PageView pageView);

        string GetColumnFromValue(string value, int limit = 16);
    }
}
