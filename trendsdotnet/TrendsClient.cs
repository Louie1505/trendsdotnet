using System.Threading.Tasks;
using trendsdotnet.Models.Responses;
using Trendsdotnet.Models;

namespace Trendsdotnet
{
    public class TrendsClient
    {
        public TrendsClient()
        {
            //TODO
        }
        public async Task<TimelineData> GetInterestOverTime(string[] terms) 
        {
            string json = await GetInterestOverTimeJSON(terms);
            using ResponseParser parser = new ResponseParser();
            return (TimelineData)(await parser.Parse(json));
        }
        public async Task<string> GetInterestOverTimeJSON(string[] terms)
        {
            Models.Payloads.Multiline payload = new Models.Payloads.Multiline();
            for (int i = 0; i < terms.Length; i++)
            {
                payload.comparisonItem.Add(new ComparisonItem(terms[i], "US"));
            }
            Request req = new Request(RequestType.Multiline, "en-US", "0", payload);
            return await req.Send();
        }
        public async Task<RegionMap> GetInterestByRegion(string[] terms) 
        {
            string json = await GetInterestByRegionJSON(terms);
            using ResponseParser parser = new ResponseParser();
            return (RegionMap)(await parser.Parse(json));
        }
        public async Task<string> GetInterestByRegionJSON(string[] terms) 
        {
            return null;
        }
        public async Task<RelatedQueries> GetRelatedQueries(string[] terms) 
        {
            string json = await GetRelatedQueriesJSON(terms);
            using ResponseParser parser = new ResponseParser();
            return (RelatedQueries)(await parser.Parse(json));
        }
        public async Task<string> GetRelatedQueriesJSON(string[] terms) 
        {
            return null;
        }
    }
}
