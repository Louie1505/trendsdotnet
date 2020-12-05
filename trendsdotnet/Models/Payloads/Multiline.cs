using Newtonsoft.Json;
using System;

namespace Trendsdotnet.Models.Payloads
{
    class Multiline : Payload
    {
        [JsonProperty(Order = 1)]
        public DateTime time { get; set; } = DateTime.Now;

        [JsonProperty(Order = 2)]
        public string resolution { get; set; }

        [JsonProperty(Order = 3)]
        public string locale { get; set; }
    }
}
