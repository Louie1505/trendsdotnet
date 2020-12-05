using System.Text.Json.Serialization;

namespace Trendsdotnet.Models
{
    class ComparisonItemComplex : IComparisonItem
    {
        public RestrictionGeo geo { get; set; }
        public ComplexKeywordsRestriction complexKeywordsRestriction { get; set; }
        public ComparisonItemComplex(string term, string geo)
        {
            //this.geo = new RestrictionGeo() { country = geo };
            this.complexKeywordsRestriction = new ComplexKeywordsRestriction(term);
        }
    }
    class ComplexKeywordsRestriction 
    {
        public ComplexKeyword[] keyword { get; set; }
        public ComplexKeywordsRestriction(string term)
        {
            this.keyword = new ComplexKeyword[] { new ComplexKeyword() { value = term } };
        }
    }
    class ComplexKeyword
    {
        public string type { get; set; } = "BROAD";
        [JsonIgnore]
        private string _value { get; set; }
        public string value { get => _value; set { _value = value.Replace(" ", "+"); } }
    }
}
