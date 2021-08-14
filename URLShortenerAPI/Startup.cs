using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using URLShortenerAPI.DataAccess;
using URLShortenerAPI.Logic;

[assembly: FunctionsStartup(typeof(URLShortenerAPI.Startup))]

namespace URLShortenerAPI
{
    public class Startup : FunctionsStartup
    {
        /// <summary>
        /// Add dependancy injection services
        /// Documentation: https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationInsightsTelemetry(Environment.GetEnvironmentVariable("AppInsights_InstrumentationKey", EnvironmentVariableTarget.Process));
            builder.Services.AddTransient<ISQLHelper, SQLHelper>();
            builder.Services.AddTransient<IURLShortenerLogic, URLShortenerLogic>();
        }
    }
}