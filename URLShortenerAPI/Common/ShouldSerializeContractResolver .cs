﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace URLShortenerAPI.Common
{
    /// <summary>
    /// https://newbedev.com/can-newtonsoft-json-net-skip-serializing-empty-lists
    /// </summary>
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string))
            {
                if (property.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    property.ShouldSerialize =
                        instance => (instance?.GetType().GetProperty(property.PropertyName).GetValue(instance) as IEnumerable<object>)?.Count() > 0;
            }
            return property;
        }
    }
}