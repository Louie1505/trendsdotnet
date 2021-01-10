using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trendsdotnet.Models.Responses;
using Trendsdotnet.Models;

namespace Trendsdotnet
{
    public class TrendsClient
    {
        // Need to know what the start of the JSON is, depends what request type we're parsing
        private readonly Dictionary<RequestType, string> reqTypeJsonMap = new Dictionary<RequestType, string>
        {
            { RequestType.Explore, "\"widgets\"" },
            { RequestType.Multiline, "\"default\"" },
            { RequestType.ComparedGeo, "\"default\"" }
        };

        public async Task<InterestTimeline> GetInterestOverTime(string[] terms, DateTime? fromDate = null, DateTime? toDate = null, string resolution = Resolution.WEEK)
        {
            string json = await GetInterestOverTimeJSON(terms, fromDate, toDate, resolution);
            using ResponseParser parser = new ResponseParser();
            return (InterestTimeline)(await parser.Parse(json, RequestType.Multiline));
        }

        public async Task<string> GetInterestOverTimeJSON(string[] terms, DateTime? fromDate = null, DateTime? toDate = null, string resolution = Resolution.WEEK)
        {
            Models.Payloads.Multiline payload = new Models.Payloads.Multiline();
            payload.resolution = resolution;
            payload.locale = "en-US";
            payload.requestOptions = new RequestOptions() { backend = "IZG", property = string.Empty };
            payload.time = $"{fromDate ?? DateTime.Now.AddYears(-1):yyyy-MM-dd}+{toDate ?? DateTime.Now:yyyy-MM-dd}";
            for (int i = 0; i < terms.Length; i++)
            {
                payload.comparisonItem.Add(new ComparisonItemComplex(terms[i], "US"));
            }
            Request req = new Request(RequestType.Multiline, "en-US", "0", payload, Request.GetTokenForRequest(terms, RequestType.Multiline).Result);
            string json = await req.Send();
            return json?.Substring(json.IndexOf(reqTypeJsonMap[RequestType.Multiline]) - 1);
        }

        public async Task<RegionInterestMap> GetInterestByRegion(string[] terms, DateTime? fromDate = null, DateTime? toDate = null, string resolution = Resolution.COUNTRY, string dataMode = DataMode.PERCENTAGES)
        {
            string json = await GetInterestByRegionJSON(terms, fromDate, toDate, resolution, dataMode);
            using ResponseParser parser = new ResponseParser();
            return (RegionInterestMap)(await parser.Parse(json, RequestType.ComparedGeo));
        }

        public async Task<string> GetInterestByRegionJSON(string[] terms, DateTime? fromDate = null, DateTime? toDate = null, string resolution = Resolution.COUNTRY, string dataMode = DataMode.PERCENTAGES)
        {
            Models.Payloads.ComparedGeo payload = new Models.Payloads.ComparedGeo();
            payload.resolution = resolution;
            payload.locale = "en-US";
            payload.requestOptions = new RequestOptions() { backend = "IZG", property = string.Empty };
            payload.dataMode = dataMode;
            for (int i = 0; i < terms.Length; i++)
            {
                payload.comparisonItem.Add(new ComparisonItemGeo(terms[i], "US") { time = $"{fromDate ?? DateTime.Parse("2004-01-01"):yyyy-MM-dd}+{toDate ?? DateTime.Now:yyyy-MM-dd}"});
            }
            Request req = new Request(RequestType.ComparedGeo, "en-US", "0", payload, Request.GetTokenForRequest(terms, RequestType.ComparedGeo).Result);
            string json = await req.Send();
            return json?.Substring(json.IndexOf(reqTypeJsonMap[RequestType.ComparedGeo]) - 1);
        }

        public async Task<RelatedQueries> GetRelatedQueries(string[] terms)
        {
            string json = await GetRelatedQueriesJSON(terms);
            using ResponseParser parser = new ResponseParser();
            return (RelatedQueries)(await parser.Parse(json, RequestType.RelatedSearches));
        }

        public async Task<string> GetRelatedQueriesJSON(string[] terms)
        {
            return null;
        }
    }
    public class Resolution
    {
        public const string MONTH = "MONTH";
        public const string WEEK = "WEEK";
        public const string COUNTRY = "COUNTRY";
    }
    public class DataMode
    {
        public const string PERCENTAGES = "PERCENTAGES";
    }
}
