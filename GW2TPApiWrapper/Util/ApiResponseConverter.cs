using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GW2Trader.ApiWrapper.Entities;

namespace GW2Trader.ApiWrapper.Util
{
    public class ApiResponseConverter
    {
        public static T DeserializeStream<T>(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            JsonReader reader = new JsonTextReader(sr);
            JsonSerializer serializer = new JsonSerializer();
            T obj = serializer.Deserialize<T>(reader);
            stream.Dispose();
            return obj;
        }
    }
}