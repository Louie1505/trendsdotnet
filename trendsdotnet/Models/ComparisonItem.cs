using System.Text.Json.Serialization;

namespace trendsdotnet.Models
{
    class ComparisonItem
    {
        [JsonIgnore]
        private string _keyword { get; set; }
        public string keyword { get => _keyword; set { _keyword = value.Replace(" ", "+"); } }
        public string geo { get; set; }
        public string time { get; set; }
    }//{"keyword":"Valentines+Day","geo":"US","time":"today+12-m"}
}
