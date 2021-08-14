namespace URLShortenerAPI.Models
{
    /// <summary>
    /// Sealed class as Response & Request objects should be independant and not inherited
    /// </summary>
    public sealed class MinifiedURLResponse
    {
        public string ShortURLId { get; set; }
    }
}