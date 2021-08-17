using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace URLShortener.Test
{
    [TestClass]
    public class URLMinifyTests : TestBase
    {
        /// <summary>
        /// Test method to ensure response is OK & content is as expected
        /// Could deserialize and access same client side model to ensure functions as expected
        /// </summary>
        [TestMethod]
        public void GetLongURL()
        {
            var request = TestFactory.CreateHttpRequest();
            var response = urlShortenerFunc.GetLongURL(request, "EFFAF6E", logger);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ContentResult)response.Result).StatusCode);
            StringAssert.Contains("{\r\n  \"LongURL\": \"https://www.google.com/\"\r\n}", ((ContentResult)response.Result).Content);
        }

        /// <summary>
        /// Test method to ensure response is OK & content is as expected
        /// Could deserialize and access same client side model to ensure functions as expected
        /// </summary>
        [TestMethod]
        public void SubmitLongURL()
        {
            var bodyData = new
            {
                longURL = "https://www.google.com/"
            };
            var request = TestFactory.CreateHttpRequest(JsonConvert.SerializeObject(bodyData).ToStream());
            var response = urlShortenerFunc.CreateMinifiedURL(request, logger);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ContentResult)response.Result).StatusCode);
            StringAssert.Contains("{\r\n  \"ShortURLId\": \"EFFAF6E\"\r\n}", ((ContentResult)response.Result).Content);
        }
    }
}