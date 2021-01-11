using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trendsdotnet.Models.Responses;
using Trendsdotnet.Models;

namespace Trendsdotnet
{
    public class TrendsClient : IDisposable
    {
        /// <summary>
        /// Get relative scores to compare several terms over time, parsed into an easy to use object.
        /// </summary>
        /// <param name="terms">Collection of terms to compare</param>
        /// <param name="from">Date to search from</param>
        /// <param name="to">Date to search to</param>
        /// <param name="resolution">Data point resolution over time period</param>
        /// <returns>Inflated InterestTimeline object</returns>
        public async Task<InterestTimeline> GetInterestOverTime(string[] terms, DateTime? from = null, DateTime? to = null, string resolution = Resolution.WEEK)
        {
            string json = await GetInterestOverTimeJSON(terms, from, to, resolution);
            using ResponseParser parser = new ResponseParser();
            return (InterestTimeline)(await parser.Parse(json, RequestType.Multiline));
        }

        /// <summary>
        /// Get relative scores to compare several terms over time, as raw JSON from the API.
        /// </summary>
        /// <param name="terms">Collection of terms to compare</param>
        /// <param name="from">Date to search from</param>
        /// <param name="to">Date to search to</param>
        /// <param name="resolution">Data point resolution over time period</param>
        /// <returns>JSON string</returns>
        public async Task<string> GetInterestOverTimeJSON(string[] terms, DateTime? from = null, DateTime? to = null, string resolution = Resolution.WEEK)
        {
            Models.Payloads.Multiline payload = new Models.Payloads.Multiline();
            payload.resolution = resolution;
            payload.locale = "en-US";
            payload.requestOptions = new RequestOptions() { backend = "IZG", property = string.Empty };
            payload.time = $"{from ?? DateTime.Now.AddYears(-1):yyyy-MM-dd}+{to ?? DateTime.Now:yyyy-MM-dd}";
            for (int i = 0; i < terms.Length; i++)
            {
                payload.comparisonItem.Add(new Models.ComparisonItems.Multiline(terms[i], "US"));
            }
            Request req = new Request(RequestType.Multiline, "en-US", "0", payload, Request.GetTokenForRequest(terms, RequestType.Multiline).Result);
            string json = await req.Send();
            return json?.Substring(json.IndexOf("\"default\"") - 1);
        }

        /// <summary>
        /// Retrieve data on a set of terms' popularity broken down by region, parsed into an easy to use object.
        /// </summary>
        /// <param name="terms">Collection of terms to compare</param>
        /// <param name="from">Date to search from</param>
        /// <param name="to">Date to search to</param>
        /// <param name="resolution">Geographical breakdown resolution</param>
        /// <param name="dataMode">Format of relative scores</param>
        /// <returns>Inflated RegionInterestMap object</returns>
        public async Task<RegionInterestMap> GetInterestByRegion(string[] terms, DateTime? from = null, DateTime? to = null, string resolution = Resolution.COUNTRY, string dataMode = DataMode.PERCENTAGES)
        {
            string json = await GetInterestByRegionJSON(terms, from, to, resolution, dataMode);
            using ResponseParser parser = new ResponseParser();
            return (RegionInterestMap)(await parser.Parse(json, RequestType.ComparedGeo));
        }

        /// <summary>
        /// Retrieve data on a set of terms' popularity broken down by region, as raw JSON from the API.
        /// </summary>
        /// <param name="terms">Collection of terms to compare</param>
        /// <param name="from">Date to search from</param>
        /// <param name="to">Date to search to</param>
        /// <param name="resolution">Geographical breakdown resolution</param>
        /// <param name="dataMode">Format of relative scores</param>>
        /// <returns>Raw JSON string</returns>
        public async Task<string> GetInterestByRegionJSON(string[] terms, DateTime? from = null, DateTime? to = null, string resolution = Resolution.COUNTRY, string dataMode = DataMode.PERCENTAGES)
        {
            Models.Payloads.ComparedGeo payload = new Models.Payloads.ComparedGeo();
            payload.resolution = resolution;
            payload.locale = "en-US";
            payload.requestOptions = new RequestOptions() { backend = "IZG", property = string.Empty };
            payload.dataMode = dataMode;
            for (int i = 0; i < terms.Length; i++)
            {
                payload.comparisonItem.Add(new Models.ComparisonItems.Geo(terms[i], "US") { time = $"{from ?? DateTime.Parse("2004-01-01"):yyyy-MM-dd}+{to ?? DateTime.Now:yyyy-MM-dd}"});
            }
            Request req = new Request(RequestType.ComparedGeo, "en-US", "0", payload, Request.GetTokenForRequest(terms, RequestType.ComparedGeo).Result);
            string json = await req.Send();
            return json?.Substring(json.IndexOf("\"default\"") - 1);
        }

        /// <summary>
        /// Retrieve frequently searched queries related to a given search term, ranked by popularity, parsed into an easy to use object.
        /// </summary>
        /// <param name="term">The term to search against</param>
        /// <param name="from">When to begin the search</param>
        /// <param name="to">When to search to</param>
        /// <returns>Inflated RankedQueryList object</returns>
        public async Task<RankedQueryList> GetRelatedQueries(string term, DateTime? from = null, DateTime? to = null)
        {
            string json = await GetRelatedQueriesJSON(term, from, to);
            using ResponseParser parser = new ResponseParser();
            return (RankedQueryList)(await parser.Parse(json, RequestType.RelatedSearches));
        }

        /// <summary>
        /// Retrieve frequently searched queries related to a given search term, ranked by popularity, as raw JSON from the API.
        /// </summary>
        /// <param name="term">The term to search against</param>
        /// <param name="from">When to begin the search</param>
        /// <param name="to">When to search to</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string> GetRelatedQueriesJSON(string term, DateTime? from = null, DateTime? to = null)
        {
            Models.Payloads.RelatedQueries payload = new Models.Payloads.RelatedQueries();
            payload.requestOptions = new RequestOptions() { backend = "IZG", property = string.Empty };
            payload.restriction = new Models.Payloads.RelatedSearchesRestriction();
            payload.restriction.complexKeywordsRestriction = new Models.ComparisonItems.RelatedQueries(term);
            payload.restriction.time = $"{from ?? DateTime.Parse("2004-01-01"):yyyy-MM-dd}+{to ?? DateTime.Now:yyyy-MM-dd}";
            Request req = new Request(RequestType.RelatedSearches, "en-US", "0", payload, Request.GetTokenForRequest(new string[] { term }, RequestType.RelatedSearches).Result);
            string json = await req.Send();
            return json?.Substring(json.IndexOf("\"default\"") - 1);
        }

        public void Dispose()
        {
            //TODO
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
