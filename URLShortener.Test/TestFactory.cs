using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;

namespace URLShortener.Test
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/azure-functions/functions-test-a-function
    /// Using microsoft documentation for building logger
    /// </summary>
    public class TestFactory
    {
        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] { "name", "Bill" },
                new object[] { "name", "Paul" },
                new object[] { "name", "Steve" }
            };
        }

        public enum LoggerTypes
        {
            Null,
            List
        }

        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key, value }
            };
            return qs;
        }

        public static HttpRequest CreateHttpRequest()
            => CreateHttpRequest(null, null);

        public static HttpRequest CreateHttpRequest(Stream bodyStream)
            => CreateHttpRequest(null, bodyStream);

        public static HttpRequest CreateHttpRequest(Dictionary<string, StringValues> queryStringList, Stream bodyStream)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            if (queryStringList != null) { request.Query = new QueryCollection(queryStringList); };
            if (bodyStream != null) { request.Body = bodyStream; }
            return request;
        }

        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            ILogger logger;

            if (type == LoggerTypes.List)
            {
                logger = new ListLogger();
            }
            else
            {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }

            return logger;
        }

        public static void BuildEnvVariablesFromDictionary(Dictionary<string, string> envSettings)
        {
            foreach (var item in envSettings)
            {
                Environment.SetEnvironmentVariable(item.Key, item.Value, EnvironmentVariableTarget.Process);
            }
        }
    }
}