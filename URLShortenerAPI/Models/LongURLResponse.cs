namespace URLShortenerAPI.Models
{
    /// <summary>
    /// Sealed class as Response & Request objects should be independant and not inherited
    /// </summary>
    public sealed class LongURLResponse
    {
        public string LongURL { get; set; }
    }
}