using System;
using Xunit;
using Trendsdotnet;
using trendsdotnet.Models.Responses;
using System.Diagnostics;

namespace trendstests
{
    public class ClientTester
    {
        TrendsClient client= new TrendsClient();
        [Fact]
        public void InterestOverTimeTest()
        {
            string[] terms = new string[] { "Trump", "Biden" };
            TimelineData data = client.GetInterestOverTime(terms, "WEEK").Result;
            Debug.Assert(data != null, "No data in response object. Likely additional logging above.");
        }
    }
}
