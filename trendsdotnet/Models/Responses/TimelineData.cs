using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Trendsdotnet;

namespace trendsdotnet.Models.Responses
{
    public class TimelineData : IResponse
    {
        [JsonProperty("timelineData")]
        public TimelineDataItem[] DataItems { get; set; }

        [JsonProperty("averages")]
        public int[] Averages { get; set; }
    }
    public class TimelineDataItem
    {
        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("formattedTime")]
        public string FormattedTimeSpan { get; set; }

        [JsonProperty("formattedAxisTime")]
        public string FormattedAxisTime { get; set; }

        [JsonProperty("value")]
        public int[] Values { get; set; }

        [JsonProperty("hasData")]
        public bool[] DataAvailable { get; set; }

        [JsonProperty("isPartial")]
        public bool PartialData { get; set; }
    }
}
