using Newtonsoft.Json;
using Trendsdotnet;

namespace Trendsdotnet.Models.Responses
{
    public class RegionInterestMap : IResponse
    {
        [JsonProperty("geoMapData")]
        RegionData[] MapData { get; set; }
    }
    public class RegionData
    {
        [JsonProperty("geoCode")]
        public string RegionCode { get; set; }

        [JsonProperty("geoName")]
        public string RegionName { get; set; }

        [JsonProperty("value")]
        public int[] Values { get; set; }

        [JsonProperty("hasData")]
        public bool[] DataAvailable { get; set; }
    }
}
