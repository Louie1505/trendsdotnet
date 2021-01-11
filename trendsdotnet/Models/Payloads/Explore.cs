using Newtonsoft.Json;
using System.Collections.Generic;

namespace Trendsdotnet.Models.Payloads
{
    class Explore : IPayload
    {
        [JsonProperty(Order = 1)]
        public List<IComparisonItem> comparisonItem = new List<IComparisonItem>();

        [JsonProperty(Order = 2)]
        public RequestOptions requestOptions = new RequestOptions();
        public int category { get; set; }
        public string property { get; set; }
    }
}
