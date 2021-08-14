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
                return Redirect(response.LongURL);
            }

            return View();
        }

        [HttpPost]
        public async Task<string> Submit(string longURL)
        {
            var response = await URLShortenerService.CreateMinifiedURLID(longURL);
            return response.ShortURLId;

        }
    }
}