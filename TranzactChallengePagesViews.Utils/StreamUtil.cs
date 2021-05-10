using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Service.Interfaces;

namespace TranzactChallengePagesViews.Utils
{
    public class StreamUtil : IStreamUtil
    {
        public MemoryStream DecompressFromStream(Stream stream)
        {
            var output = new MemoryStream();
            using (var zipStream = new GZipStream(stream, CompressionMode.Decompress))
            {
                zipStream.CopyTo(output);
                zipStream.Close();
                output.Position = 0;
                return output;
            }
        }

        public async Task<List<string>> ReadLinesFromStream(MemoryStream stream,
                                     Encoding encoding)
        {
            List<string> lines = new List<string>();

            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
