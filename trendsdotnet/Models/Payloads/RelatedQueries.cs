using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json.Serialization;

namespace Trendsdotnet.Models.Payloads
{
    class RelatedQueries : IPayload
    {
        [JsonProperty(Order = 1)]
        public RelatedSearchesRestriction restriction { get; set; }

        [JsonProperty(Order = 2)]
        public string keywordType { get; set; } = "QUERY";

        [JsonProperty(Order = 3)]
        public string[] metric { get; set; } = { "TOP", "RISING" };

        [JsonProperty(Order = 4)]
        public TrendinessSettings trendinessSettings { get; set; } = new TrendinessSettings();

        [JsonProperty(Order = 5)]
        public RequestOptions requestOptions = new RequestOptions();

        [JsonProperty(Order = 6)]
        public string language { get; set; } = "en";

        [JsonProperty(Order = 7)]
        public string userCountryCode { get; set; } = "US";
    }
    class TrendinessSettings
    {
        //99% sure this isn't used at all by the API since the actual time is on the restriction, but guess what's returned if you don't send it 🙃
        public string compareTime { get; set; } = "2004-01-01+2005-01-01";
    }
    class RelatedSearchesRestriction 
    {
        public JObject geo { get; set; } = new JObject();
        public string time { get; set; }
        public string originalTimeRangeForExploreUrl { get; set; } = "all";
        public IComparisonItem complexKeywordsRestriction { get; set; }
    }
}
