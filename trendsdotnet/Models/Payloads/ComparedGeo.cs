using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Trendsdotnet.Models.Payloads
{
    class ComparedGeo : Payload
    {
        //public RestrictionGeo geo { get; set; }
        [JsonProperty(Order = 2)]
        public JObject geo { get; set; } = new JObject();

        [JsonProperty(Order = 6)]
        public string resolution { get; set; }

        [JsonProperty(Order = 7)]
        public string locale { get; set; }
        [JsonProperty(Order = 9)]
        public string dataMode { get; set; }
    }
}
