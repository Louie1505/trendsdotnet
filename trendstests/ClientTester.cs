using System;
using Xunit;
using Trendsdotnet;

namespace trendstests
{
    public class ClientTester
    {
        TrendsClient client= new TrendsClient();
        [Fact]
        public void InterestOverTimeTest()
        {
            string[] terms = new string[] { "Trump", "Biden" };
            client.GetInterestOverTime(terms, "WEEK");
        }
    }
}
