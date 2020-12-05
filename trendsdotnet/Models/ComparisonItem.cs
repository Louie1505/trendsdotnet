using Newtonsoft.Json;

namespace Trendsdotnet.Models
{
    interface IComparisonItem { }
    class ComparisonItem : IComparisonItem
    {
        [JsonIgnore]
        private string _keyword { get; set; }
        public string keyword { get => _keyword; set { _keyword = value.Replace(" ", "+"); } }
        public string geo { get; set; }
        public string time { get; set; }
        public ComparisonItem(string term, string geo)
        {
            this.keyword = term;
            this.geo = geo;
            this.time = "today+12-m";
        }
    }
}
