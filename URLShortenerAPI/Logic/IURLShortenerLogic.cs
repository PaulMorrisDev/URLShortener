using URLShortenerAPI.Models;

namespace URLShortenerAPI.Logic
{
    public interface IURLShortenerLogic
    {
        public LongURLResponse GetLongURL(string shortURL);

        public MinifiedURLResponse CreateMinifiedURL(string longURL);
    }
}