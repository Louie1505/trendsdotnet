using System.Collections.Generic;

namespace Trendsdotnet.Models.Payloads
{
    abstract class Payload
    {
        public List<ComparisonItem> comparisonItem = new List<ComparisonItem>();
        public Restriction restriction = new Restriction();
        public RequestOptions requestOptions = new RequestOptions();
    }
}
