using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Configuration;

namespace TranzactChallengePagesViews
{
    public class Program
    {
        public static void Main()
        {
            var services = Startup.Setup();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Main>().Run();
        }
    }
}

