using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;

namespace URLShortenerAPI.Common
{
    public static class RequestHandler
    {
        public static T GetBodyContentInObject<T>(HttpRequest req) where T : new()
            => JsonConvert.DeserializeObject<T>(GetBodyContent(req));

        public static string GetBodyContent(HttpRequest req)
        {
            var bodyContent = new StreamReader(req.Body).ReadToEndAsync()?.Result?.ToString();

            if (string.IsNullOrWhiteSpace(bodyContent))
            {
                throw new Exception("Request body is empty");
            }
            return bodyContent;
        }
    }
}