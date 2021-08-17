using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using URLShortenerAPI.Common;
using URLShortenerAPI.Logic;
using URLShortenerAPI.Models;

namespace URLShortenerAPI
{
    public class URLShortenerAPI
    {
        private readonly IURLShortenerLogic urlShortenerLogic;

        public URLShortenerAPI(IURLShortenerLogic _urlShortenerLogic)
        {
            urlShortenerLogic = _urlShortenerLogic;
        }

        /// <summary>
        /// Method for retrieving Long URL
        /// </summary>
        /// <param name="req"></param>
        /// <param name="shortURL"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GetLongURL")]
        public async Task<IActionResult> GetLongURL(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{shortURL}")] HttpRequest req, string shortURL,
            ILogger log)
        {
            try
            {
                var response = ResponseHandler.CreateJSONResponse(urlShortenerLogic.GetLongURL(shortURL));
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return ResponseHandler.CreateJSONResponse(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Method for creating Short URL ID and storing in SQL Database
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns>Short URL ID</returns>
        [FunctionName("CreateMinifiedURL")]
        public async Task<IActionResult> CreateMinifiedURL(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            CreateMinifiedURLRequest requestObj = null;
            try
            {
                requestObj = RequestHandler.GetBodyContentInObject<CreateMinifiedURLRequest>(req);
                var response = ResponseHandler.CreateJSONResponse(urlShortenerLogic.CreateMinifiedURL(requestObj.LongURL));
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return ResponseHandler.CreateJSONResponse(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}