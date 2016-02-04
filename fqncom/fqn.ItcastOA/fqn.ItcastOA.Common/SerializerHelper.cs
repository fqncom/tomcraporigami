using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace fqn.ItcastOA.Common
{
    public class SerializerHelper
    {
        public static string SerializerToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T SerializerToObject<T>(string serializerString)
        {
            return JsonConvert.DeserializeObject<T>(serializerString);
        }

    }
}
