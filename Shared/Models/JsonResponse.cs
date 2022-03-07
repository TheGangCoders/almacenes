using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    
    public class JsonResponse<T>
    {
        [JsonProperty("ok", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? Ok { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public String Message { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public T Result { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<T> Data { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public Int32? Total { get; set; }

        [JsonProperty("dynamic", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Dynamic { get; set; }
    }
}
