using System.Collections.Generic;

namespace URLShortenerAPI.DataAccess
{
    public interface ISQLHelper
    {
        public string GetData(string storedProcedure, Dictionary<string, string> parameters);
    }
}