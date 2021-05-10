using Flurl.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Service.Interfaces;

namespace TranzactChallengePagesViews.Service
{
    public class DownloadUtil : IDownloadUtil
    {
        public async Task<Stream> GetPageViewStream(string linkToDownload)
        {
            return await linkToDownload.WithTimeout(TimeSpan.FromMinutes(10)).GetStreamAsync();
        }
    }
}
