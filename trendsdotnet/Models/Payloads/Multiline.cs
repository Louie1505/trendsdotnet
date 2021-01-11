using Newtonsoft.Json;
using System.Collections.Generic;

namespace Trendsdotnet.Models.Payloads
{
    class Multiline : IPayload
    {
        [JsonProperty(Order = 1)]
        public string time { get; set; }

        [JsonProperty(Order = 2)]
        public string resolution { get; set; }

        [JsonProperty(Order = 3)]
        public string locale { get; set; }

        [JsonProperty(Order = 5)]
        public List<IComparisonItem> comparisonItem = new List<IComparisonItem>();

        [JsonProperty(Order = 6)]
        public RequestOptions requestOptions = new RequestOptions();
    }
}
