using Newtonsoft.Json;

namespace URLShortenerAPI.Common
{
    internal class JsonSettings
    {
        /// <summary>
        /// Skip serializing empty lists
        /// https://newbedev.com/can-newtonsoft-json-net-skip-serializing-empty-lists
        /// </summary>
        public static readonly JsonSerializerSettings IgnoreEmptySettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = ShouldSerializeContractResolver.Instance,
        };
    }
}