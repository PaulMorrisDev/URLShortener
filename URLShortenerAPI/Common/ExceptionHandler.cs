using System;
using System.Net;

namespace URLShortenerAPI.Common
{
    /// <summary>
    /// Custom implementation of exception
    /// Prevents compilor error CS0144
    /// https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0144?f1url=%3FappId%3Droslyn%26k%3Dk(CS0144)
    /// </summary>
    internal class ExceptionHandler : Exception
    {
        /// <summary>
        /// Handle and prepare exception
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="exceptionMessage"></param>
        /// <param name="detailedMessage"></param>
        public ExceptionHandler(HttpStatusCode statusCode, string exceptionMessage, string detailedMessage = null) : base(exceptionMessage)
        {
            base.Data.Add("StatusCode", statusCode);
            if (string.IsNullOrWhiteSpace(detailedMessage))
            {
                base.Data.Add("DetailedMessage", detailedMessage);
            }
        }
    }
}