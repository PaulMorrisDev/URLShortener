namespace URLShortenerAPI.Models
{
    /// <summary>
    /// Sealed class as Response & Request objects should be independant and not inherited
    /// </summary>
    public sealed class CreateMinifiedURLRequest
    {
        public string LongURL { get; set; }
    }
}