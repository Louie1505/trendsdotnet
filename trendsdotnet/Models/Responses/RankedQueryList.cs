using Newtonsoft.Json;
using Trendsdotnet;

namespace Trendsdotnet.Models.Responses
{
    public class RankedQueryList : IResponse
    {
        [JsonProperty("rankedKeyword")]
        public Keyword[] Keywords { get; set; }
    }
    public class Keyword 
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("hasData")]
        public bool DataAvailable { get; set; }
    }
}
