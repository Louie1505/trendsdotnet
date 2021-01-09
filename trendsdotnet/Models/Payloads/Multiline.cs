using Newtonsoft.Json;
using System;

namespace Trendsdotnet.Models.Payloads
{
    class Multiline : Payload
    {
        [JsonProperty(Order = 1)]
        public string time { get; set; } = $"{DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd")}+{DateTime.Now.ToString("yyyy-MM-dd")}";

        [JsonProperty(Order = 2)]
        public string resolution { get; set; }

        [JsonProperty(Order = 3)]
        public string locale { get; set; }
    }
}
