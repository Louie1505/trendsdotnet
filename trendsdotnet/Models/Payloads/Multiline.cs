using System;

namespace trendsdotnet.Models.Payloads
{
    class Multiline : Payload
    {
        public DateTime time { get; set; }
        public string resolution { get; set; }
        public string locale { get; set; }
    }
}
