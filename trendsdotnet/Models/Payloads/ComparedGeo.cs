using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Trendsdotnet.Models.Payloads
{
    class ComparedGeo : IPayload
    {
        //public RestrictionGeo geo { get; set; }
        [JsonProperty(Order = 1)]
        public JObject geo { get; set; } = new JObject();

        [JsonProperty(Order = 2)]
        public List<IComparisonItem> comparisonItem = new List<IComparisonItem>();

        [JsonProperty(Order = 3)]
        public string resolution { get; set; }

        [JsonProperty(Order = 4)]
        public string locale { get; set; }

        [JsonProperty(Order = 5)]
        public RequestOptions requestOptions = new RequestOptions();

        [JsonProperty(Order = 6)]
        public string dataMode { get; set; }
    }
}
