using Newtonsoft.Json;

namespace URLShortenerWebApp.Models
{
    public class MinifiedURLResponse
    {
        [JsonProperty("LongURL")]
        public string LongURL { get; set; }

        [JsonProperty("ShortURLId")]
        public string ShortURLId { get; set; }
    }
}