using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PressYourLuck.Models
{
    public static class SessionExtensions
    {
        public static void SetObject<P>(this ISession session, string key, P value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static P GetObject<P>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return (string.IsNullOrEmpty(value)) ? default(P) :
                JsonConvert.DeserializeObject<P>(value);
        }
    }
}
