﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using URLShortenerWebApp.Models;

namespace URLShortenerWebApp.Services
{
    public class URLShortenerService
    {
        private static HttpClient client = new HttpClient();

        public static async Task<MinifiedURLResponse> CreateMinifiedURLID(string longURL)
        {
            MinifiedURLResponse responseObj = null;
            string apiResponse = string.Empty;
            try
            {
                //longURL = HttpUtility.UrlEncode(longURL);
                var httpContent = new StringContent(JsonConvert.SerializeObject(new MinifiedURLRequest() { LongURL = longURL }));
                client.DefaultRequestHeaders.Add("ocp-apim-subscription-key", "f2d3f534dce045ecbb6c093df90203d0");

                HttpResponseMessage response = await client.PostAsync(new Uri("https://apim-urlshortener.azure-api.net/paulmorris-urlshortenerapi/CreateMinifiedURL"), httpContent);

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

        public static async Task<MinifiedURLResponse> GetFullURL(string shortURLID)
        {
            MinifiedURLResponse responseObj = null;
            string apiResponse = string.Empty;
            try
            {
                client.DefaultRequestHeaders.Add("ocp-apim-subscription-key", "f2d3f534dce045ecbb6c093df90203d0");

                HttpResponseMessage response = await client.GetAsync($"https://apim-urlshortener.azure-api.net/paulmorris-urlshortenerapi/{shortURLID}");

                if (response.IsSuccessStatusCode)
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
                responseObj = JsonConvert.DeserializeObject<MinifiedURLResponse>(apiResponse);
                //responseObj.LongURL = HttpUtility.UrlDecode(responseObj.LongURL);
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