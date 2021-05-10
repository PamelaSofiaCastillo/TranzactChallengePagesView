using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Core.Model;
using TranzactChallengePagesViews.Service.Interfaces;

namespace TranzactChallengePagesViews.Service
{
    public class PrintService : IPrintService
    {
        public void PrintResults(List<PageView> pageViews)
        {
            Console.WriteLine($"{GetColumnFromValue("Domain_code")}{GetColumnFromValue("Page_title", 25)}{GetColumnFromValue("Count_views")}");
            foreach(var pageView in pageViews)
            {
               Console.WriteLine(GetRowToPrint(pageView));
            }
        }

        public string GetRowToPrint(PageView pageView)
        {
            return $"{GetColumnFromValue(pageView.DomainCode)}{GetColumnFromValue(pageView.PageTitle, 25)}{GetColumnFromValue(pageView.CountViews.ToString())}";
        }

        public string GetColumnFromValue(string value, int limit = 16)
        {
            if (value == null) return new string(' ', limit);
            else if (value.Length > limit) return value.Substring(0, limit);
            return $"{value}{new string(' ', limit - value.Length)}";
        }
    }
}
