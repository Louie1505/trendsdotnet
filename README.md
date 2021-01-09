# trendsdotnet
Unofficial C# wrapper around the Google Trends API (https://trends.google.com/)

## Description
C# assembly for consuming Google Trends API. Handles authentication and returns data either in a complex object or as the raw JSON returned from the API. 
Functionality to compare a collection of terms over a timeline, breakdown interest in terms by region, or retrieve top related queries (ie search suggestions) for a set of terms.

## Example usages


#### All functions require a TrendsClient creating:
`TrendsClient client = new TrendsClient();`


### Interest over time

#### Store the terms to be compared in a string array
`string[] terms = new string[] { "Google", "Bing" };`

#### Either retrieve the raw JSON data from the API, to be used as needed. Dates and resolution are optional, and default the the below.
`string json = await client.GetInterestOverTimeJSON(terms, DateTime.Now.AddYears(-1), DateTime.Now, Resolution.WEEK);`

#### OR recieve the data parsed into a complex object. Dates and resolution are optional, and default the the below.
`TimelineData data = await client.GetInterestOverTime(terms, DateTime.Now.AddYears(-1), DateTime.Now, Resolution.WEEK);`
#### TimelineData contains `Averages` which are average relative scores for the terms, and `DataItems` which are data points of the collection in time order. All scores are in the order the terms were entered, ie `data.DataItems[x].Values[0]` is the score of the 1st term and `data.DataItems[x].Values[1]` is the score of the 2nd when comparing 2 terms.
