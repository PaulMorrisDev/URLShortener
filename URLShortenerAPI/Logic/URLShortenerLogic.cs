using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web;
using URLShortenerAPI.Common;
using URLShortenerAPI.DataAccess;
using URLShortenerAPI.Models;

namespace URLShortenerAPI.Logic
{
    public class URLShortenerLogic : IURLShortenerLogic
    {
        private readonly ISQLHelper sqlHelper;

        public URLShortenerLogic(ISQLHelper _sqlHelper)
        {
            sqlHelper = _sqlHelper;
        }

        /// <summary>
        /// Calls SQL Database to retrieve full length URL from short URL Identifier
        /// </summary>
        /// <param name="shortURL"></param>
        /// <returns>Long URL</returns>
        public LongURLResponse GetLongURL(string shortURL)
        {
            var dbresponse = sqlHelper.GetData("GetLongURL", new Dictionary<string, string>() { { "ShortURLID", shortURL } });

            var response = JsonConvert.DeserializeObject<LongURLResponse>(dbresponse);
            if (string.IsNullOrWhiteSpace(response.LongURL))
            {
                throw new ExceptionHandler(HttpStatusCode.NotFound, "No results found");
            }
            return response;
        }

        public MinifiedURLResponse CreateMinifiedURL(string longURL)
        {
            var dbResponse = sqlHelper.GetData("InsertShortURLID", new Dictionary<string, string>() { { "LongURL", longURL } });
            var response = JsonConvert.DeserializeObject<MinifiedURLResponse>(dbResponse);
            return response;
        }
    }
}