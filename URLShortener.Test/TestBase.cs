using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using URLShortenerAPI;
using URLShortenerAPI.Logic;

namespace URLShortener.Test
{
    public class TestBase
    {
        public readonly URLShortenerAPI.URLShortenerAPI urlShortenerFunc;
        public readonly ListLogger logger;

        public TestBase()
        {
            var envVariables = new Dictionary<string, string>()
            {
                { "SQLInitialCatalog","URLShortener" },
                { "SQLDataSource","paulmorris-urlshortener.database.windows.net" },
                { "SQLUserID","paulmorris" },
                { "SQLPassword","P8ssword#1234" },
                { "AzureWebJobsStorage","URLShortener" }
            };
            TestFactory.BuildEnvVariablesFromDictionary(envVariables);
            var host = new HostBuilder().ConfigureWebJobs(new Startup().Configure).Build();
            logger = (ListLogger)TestFactory.CreateLogger(TestFactory.LoggerTypes.List);
            urlShortenerFunc = new URLShortenerAPI.URLShortenerAPI(host.Services.GetRequiredService<IURLShortenerLogic>());
        }
    }
}
