using Newtonsoft.Json;

namespace Trendsdotnet.Models
{
    class ComparisonItemGeo : IComparisonItem
    {
        [JsonProperty(Order = 1)]
        public string time { get; set; }
        [JsonProperty(Order = 2)]
        public GeoKeywordsRestriction complexKeywordsRestriction { get; set; }
        public ComparisonItemGeo(string term, string geo)
        {
            //this.geo = new RestrictionGeo() { country = geo };
            this.complexKeywordsRestriction = new GeoKeywordsRestriction(term);
        }
    }
    class GeoKeywordsRestriction 
    {
        public GeoKeyword[] keyword { get; set; }
        public GeoKeywordsRestriction(string term)
        {
            this.keyword = new GeoKeyword[] { new GeoKeyword() { value = term } };
        }
    }
    class GeoKeyword
    {
        public string type { get; set; } = "BROAD";
        [System.Text.Json.Serialization.JsonIgnore]
        private string _value { get; set; }
        public string value { get => _value; set { _value = value.Replace(" ", "+"); } }
    }
}
