using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace URLShortener.Test
{
    [TestClass]
    public class URLMinifyTests : TestBase
    {
        [TestMethod]
        public void GetLongURL()
        {
            var request = TestFactory.CreateHttpRequest("ShortURLId", "EFFAF6E");
            var response = urlShortenerFunc.GetLongURL(request, "EFFAF6E", logger);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ContentResult)response.Result).StatusCode);
        }
    }
}
