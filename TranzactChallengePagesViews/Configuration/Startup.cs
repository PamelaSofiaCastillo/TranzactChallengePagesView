using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranzactChallengePagesViews.Service;
using TranzactChallengePagesViews.Service.Interfaces;
using TranzactChallengePagesViews.Utils;

namespace TranzactChallengePagesViews.Configuration
{
    public class Startup
    {
        public static IServiceCollection Setup()
        {
            var services = new ServiceCollection();

            IConfiguration configs = GetConfigs();
            services.AddSingleton(configs);

            // SERVICES
            services.AddSingleton<ILinkService, LinkService>();
            services.AddSingleton<IPageviewService, PageViewService>();
            services.AddSingleton<IPrintService, PrintService>();

            // UTILS
            services.AddSingleton<IStreamUtil, StreamUtil>();
            services.AddSingleton<IDownloadUtil, DownloadUtil>();

            // MAIN
            services.AddSingleton<Main>();

            return services;
        }

        private static IConfiguration GetConfigs()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            return builder.Build();
        }

    }
}
