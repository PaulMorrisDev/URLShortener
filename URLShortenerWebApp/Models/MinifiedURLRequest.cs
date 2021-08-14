using Newtonsoft.Json;

namespace URLShortenerWebApp.Models
{
    public class MinifiedURLRequest
    {
        [JsonProperty("LongURL")]
        public string LongURL { get; set; }
    }
}