using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Trendsdotnet.Models.ComparisonItems
{
    class RelatedQueries : IComparisonItem
    {
        public Keyword[] keyword { get; set; }
        public RelatedQueries(string term)
        {
            this.keyword = new Keyword[] { new Keyword(term) };
        }
    }
    class Keyword
    {
        public string type { get; set; } = "BROAD";
        public string value { get; set; }
        public Keyword(string term)
        {
            this.value = term;
        }
    }
}
