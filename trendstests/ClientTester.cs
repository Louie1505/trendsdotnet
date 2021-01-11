using System;
using Xunit;
using Trendsdotnet;
using trendsdotnet.Models.Responses;
using System.Diagnostics;

namespace trendstests
{
    public class ClientTester
    {
        TrendsClient client = new TrendsClient();

        [Fact]
        public void InterestOverTimeTest()
        {
            string[] terms = new string[] { "Google", "Bing" };
            InterestTimeline data = client.GetInterestOverTime(terms).Result;
            Debug.Assert(data != null, "No data in response object. Likely additional logging above.");
        }

        [Fact]
        public void InterestOverTimeJSONTest()
        {
            string[] terms = new string[] { "Google", "Bing" };
            string json = client.GetInterestOverTimeJSON(terms).Result;
            Debug.Assert(!string.IsNullOrEmpty(json), "Response JSON is empty, likely additional logging above");
        }

        [Fact]
        public void InterestByRegionTest()
        {
            string[] terms = new string[] { "Google", "Bing" };
            RegionInterestMap map = client.GetInterestByRegion(terms).Result;
            Debug.Assert(map != null, "No data in response object. Likely additional logging above.");
        }

        [Fact]
        public void InterestByRegionJSONTest()
        {
            string[] terms = new string[] { "Google", "Bing" };
            string json = client.GetInterestByRegionJSON(terms).Result;
            Debug.Assert(!string.IsNullOrEmpty(json), "Response JSON is empty, likely additional logging above");
        }

        [Fact]
        public void RelatedQueriesTest()
        {
            RelatedQueries map = client.GetRelatedQueries("Google").Result;
            Debug.Assert(map != null, "No data in response object. Likely additional logging above.");
        }

        [Fact]
        public void RelatedQueriesJSONTest()
        {
            string json = client.GetRelatedQueriesJSON("Google").Result;
            Debug.Assert(!string.IsNullOrEmpty(json), "Response JSON is empty, likely additional logging above");
        }
    }
}
