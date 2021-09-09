using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.Utilities
{
    public class JsonTool
    {
        public static string SersializeObject<T>(T t)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            return JsonConvert.SerializeObject(t, settings);
        }
        public static T DeserializeObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}
