trendsdotnet
======

Unofficial C# wrapper around the Google Trends API (https://trends.google.com/). Handles authentication and returns data either in a complex object or as the raw JSON returned from the API. 
Functionality to compare a collection of terms over a timeline, breakdown interest in terms by region, or retrieve top related queries (ie search suggestions) for a set of terms.

---

Endpoint Descriptions (Taken from Google Trends)
------

### __Interest over time__

Returns numbers representing search interest relative to the highest point on the chart for the given region and time. A value of 100 is the peak popularity for the term. A value of 50 means that the term is half as popular. A score of 0 means there was not enough data for this term.

### __Interest by region__

See which term ranked highest in each region during the specified time frame. Values are scaled from 0 to 100, where 100 is the region with peak popularity, a value of 50 is the region where the term is half as popular, and a value of 0 means that term was less than 1% as popular as the peak.

### __Related queries__

Users searching for your term also searched for these queries. Scoring is on a relative scale where a value of 100 is the most commonly searched query, 50 is a query searched half as often as the most popular query, and so on.

---

Example usages
------


#### All functions require a TrendsClient creating:
`using(TrendsClient client = new TrendsClient()) {...`


### __Interest over time__

#### Store the terms to be compared in a string array
`string[] terms = new string[] { "Google", "Bing" };`

#### Either retrieve the raw JSON data from the API, to be used as needed. Dates and resolution are optional, and default the the below.
`string json = await client.GetInterestOverTimeJSON(terms, DateTime.Now.AddYears(-1), DateTime.Now, Resolution.WEEK);`

#### OR recieve the data parsed into a complex object. Dates and resolution are optional, and default the the below.
`InterestTimeline data = await client.GetInterestOverTime(terms, DateTime.Now.AddYears(-1), DateTime.Now, Resolution.WEEK);`
#### InterestTimeline data points contain `Averages` which are average relative scores for the terms, and `DataItems` which are data points of the collection in time order. All scores are in the order the terms were entered, ie `data.DataItems[x].Values[0]` is the score of the 1st term and `data.DataItems[x].Values[1]` is the score of the 2nd when comparing 2 terms.


### __Interest by region__

#### Store the terms to be compared in a string array
`string[] terms = new string[] { "Google", "Bing" };`

#### Either retrieve the raw JSON data from the API, to be used as needed. All parameters but terms are optional, and default the the below.
`string json = await client.GetInterestByRegionJSON(terms, DateTime.Parse("2004-01-01"), DateTime.Now, Resolution.COUNTRY, DataMode.PERCENTAGES);`

#### OR recieve the data parsed into a complex object. All parameters but terms are optional, and default the the below.
`RegionInterestMap data = await client.GetInterestOverTime(terms, DateTime.Parse("2004-01-01"), DateTime.Now, Resolution.COUNTRY, DataMode.PERCENTAGES);`
#### RegionInterestMap contains 'MapData' with a collection of 'RegionData' data points, each of which contains the `RegionCode` and `RegionName`, as well as `Values` which are average relative scores for the terms. All scores are in the order the terms were entered.