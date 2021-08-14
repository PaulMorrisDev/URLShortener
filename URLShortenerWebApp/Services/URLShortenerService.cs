using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using URLShortenerWebApp.Models;

namespace URLShortenerWebApp.Services
{
    /// <summary>
    /// Service class dedicated to calling URL Shortener APIM Instance
    /// </summary>
    public class URLShortenerService
    {
        private static HttpClient client = new HttpClient();
        private static string minifiedURLAPIMURL = "https://apim-urlshortener.azure-api.net/paulmorris-urlshortenerapi/";
        private static string aPIMSubscriptionKey = "f2d3f534dce045ecbb6c093df90203d0";

        /// <summary>
        /// Call to APIM to create a Minified URL ID
        /// </summary>
        /// <param name="longURL"></param>
        /// <returns>Minifed URL ID</returns>
        public static async Task<MinifiedURLResponse> CreateMinifiedURLID(string longURL)
        {
            MinifiedURLResponse responseObj = null;
            string apiResponse = string.Empty;
            try
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(new MinifiedURLRequest() { LongURL = longURL }));

                //Retrieve this key from KeyVault
                client.DefaultRequestHeaders.Add("ocp-apim-subscription-key", aPIMSubscriptionKey);

                HttpResponseMessage response = await client.PostAsync(new Uri($"{minifiedURLAPIMURL}CreateMinifiedURL"), httpContent);

                if (response.IsSuccessStatusCode)
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
                responseObj = JsonConvert.DeserializeObject<MinifiedURLResponse>(apiResponse);
                return responseObj;
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
                return responseObj;
            }
        }

        /// <summary>
        /// Call to APIM to get full URL against minified URL ID
        /// </summary>
        /// <param name="shortURLID"></param>
        /// <returns>Full URL</returns>
        public static async Task<MinifiedURLResponse> GetFullURL(string shortURLID)
        {
            MinifiedURLResponse responseObj = null;
            string apiResponse = string.Empty;
            try
            {
                //Retrieve this key from KeyVault
                client.DefaultRequestHeaders.Add("ocp-apim-subscription-key", aPIMSubscriptionKey);
                HttpResponseMessage response = await client.GetAsync($"{minifiedURLAPIMURL}/{shortURLID}");

                if (response.IsSuccessStatusCode)
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
                responseObj = JsonConvert.DeserializeObject<MinifiedURLResponse>(apiResponse);
                return responseObj;
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
                return responseObj;
            }
        }
    }
}