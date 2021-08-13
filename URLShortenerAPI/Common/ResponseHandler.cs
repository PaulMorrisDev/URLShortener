using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace URLShortenerAPI.Common
{
    internal class ResponseHandler
    {
        public static ContentResult CreateJSONResponse(object jsonObject)
       => new ContentResult { StatusCode = (int)HttpStatusCode.OK, Content = JsonConvert.SerializeObject(jsonObject, JsonSettings.IgnoreEmptySettings), ContentType = "application/json" };

        public static ContentResult CreateJSONResponse(object jsonObject, HttpStatusCode httpStatusCode)
            => new ContentResult { StatusCode = (int)httpStatusCode, Content = JsonConvert.SerializeObject(jsonObject, JsonSettings.IgnoreEmptySettings), ContentType = "application/json" };
    }
}