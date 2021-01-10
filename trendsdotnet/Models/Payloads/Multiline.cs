using Newtonsoft.Json;

namespace Trendsdotnet.Models.Payloads
{
    class Multiline : Payload
    {
        [JsonProperty(Order = 1)]
        public string time { get; set; }

        [JsonProperty(Order = 2)]
        public string resolution { get; set; }

        [JsonProperty(Order = 3)]
        public string locale { get; set; }
    }
}
