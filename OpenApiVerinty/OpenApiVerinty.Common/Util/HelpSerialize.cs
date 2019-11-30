using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Util
{
    public static class HelpSerialize<T>
    {
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeObject(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj.ToString());
        }
    }
}
