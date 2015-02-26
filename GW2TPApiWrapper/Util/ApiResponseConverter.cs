using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GW2TPApiWrapper.Entities;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GW2TPApiWrapper.Wrapper
{
    public class ApiResponseConverter
    {
        public static T DeserializeStream<T>(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            JsonReader reader = new JsonTextReader(sr);
            JsonSerializer serializer = new JsonSerializer();

            T obj = serializer.Deserialize<T>(reader);
            stream.Close();

            return obj;
        }
    }
}
