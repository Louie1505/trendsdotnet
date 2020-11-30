namespace Trendsdotnet.Models.Payloads
{
    class ComparedGeo : Payload
    {
        public RestrictionGeo geo { get; set; }
        public string resolution { get; set; }
        public string locale { get; set; }
        public string dataMode { get; set; }
    }
}
