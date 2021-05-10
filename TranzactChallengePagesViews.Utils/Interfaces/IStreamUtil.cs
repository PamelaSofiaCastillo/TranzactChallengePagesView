using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranzactChallengePagesViews.Service.Interfaces
{
    public interface IStreamUtil
    {
        MemoryStream DecompressFromStream(Stream stream);
        Task<List<string>> ReadLinesFromStream(MemoryStream stream, Encoding encoding);
    }
}
