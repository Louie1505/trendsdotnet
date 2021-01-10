using Newtonsoft.Json;
using System.Collections.Generic;

namespace Trendsdotnet.Models.Payloads
{
    abstract class Payload
    {
        [JsonProperty(Order = 5)]
        public List<IComparisonItem> comparisonItem = new List<IComparisonItem>();

        //[JsonProperty(Order = 6)]
        //public Restriction restriction = new Restriction();

        [JsonProperty(Order = 8)]
        public RequestOptions requestOptions = new RequestOptions();
    }
}
