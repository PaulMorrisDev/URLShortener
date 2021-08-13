using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
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
                return ResponseHandler.CreateJSONResponse(ex);
            }
        }

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
                return ResponseHandler.CreateJSONResponse(ex);
            }
        }
    }
}