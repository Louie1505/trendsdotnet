using System;

namespace Trendsdotnet.Models.Payloads
{
    class Multiline : Payload
    {
        public DateTime time { get; set; } = DateTime.Now;
        public string resolution { get; set; }
        public string locale { get; set; }
    }
}
