using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using URLShortenerWebApp.Services;

namespace URLShortenerWebApp.Controllers
{
    public class URLShortenerController : Controller
    {
        public async Task<IActionResult> Index(string shortURLID)
        {
            if (!string.IsNullOrWhiteSpace(shortURLID))
            {
                var response = await URLShortenerService.GetFullURL(shortURLID);
                string longURL = response.LongURL;
                if (string.IsNullOrWhiteSpace(longURL))
                {
                    ViewBag.Error = true;
                    return View();
                }
                return Redirect(response.LongURL);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string longURL)
        {
            var response = await URLShortenerService.CreateMinifiedURLID(longURL);
            if (response == null)
            {
                ViewBag.Error = true;
            }
            else
            {
                ViewBag.url = Request.Host + "/" + response.ShortURLId;
            }
            return View("Index");
        }
    }
}