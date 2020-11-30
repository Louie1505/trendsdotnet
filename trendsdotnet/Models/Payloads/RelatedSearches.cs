using System;

namespace Trendsdotnet.Models.Payloads
{
    class RelatedSearches : Payload
    {
        public string keywordType { get; set; }
        public string[] metric { get; set; }
        public TrendinessSettings trendinessSettings { get; set; }
    }
    class TrendinessSettings
    {
        public DateTime compareTime { get; set; }
    }
}
