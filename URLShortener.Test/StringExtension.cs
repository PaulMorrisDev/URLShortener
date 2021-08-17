using System.IO;

namespace URLShortener.Test
{
    public static class StringExtension
    {
        /// <summary>
        /// Extension method to turn string in to stream object for HTTP Requests
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Stream ToStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}