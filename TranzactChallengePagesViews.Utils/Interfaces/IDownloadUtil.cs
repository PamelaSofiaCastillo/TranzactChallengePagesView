using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranzactChallengePagesViews.Service.Interfaces
{
    public interface IDownloadUtil
    {
        Task<Stream> GetPageViewStream(string linkToDownload);
    }
}
