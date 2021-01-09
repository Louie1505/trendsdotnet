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
            TimelineData data = client.GetInterestOverTime(terms).Result;
            Debug.Assert(data != null, "No data in response object. Likely additional logging above.");
        }

        [Fact]
        public void InterestOverTimeJSONTest()
        {
            string[] terms = new string[] { "Google", "Bing" };
            string json = client.GetInterestOverTimeJSON(terms).Result;
        }

        [Fact]
        public void InterestByRegionTest()
        {
            string[] terms = new string[] { "Google", "Bing" };
            RegionMap map = client.GetInterestByRegion(terms, Resolution.COUNTRY, DataMode.PERCENTAGES).Result;
            Debug.Assert(map != null, "No data in response object. Likely additional logging above.");
        }

        [Fact]
        public void InterestByRegionJSONTest()
        {
            string[] terms = new string[] { "Google", "Bing" };
            string json = client.GetInterestByRegionJSON(terms, Resolution.COUNTRY, DataMode.PERCENTAGES).Result;
        }
    }
}
