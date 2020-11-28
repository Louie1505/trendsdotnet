namespace trendsdotnet.Models.Payloads
{
    abstract class Payload
    {
        public ComparisonItem[] comparisonItem { get; set; }
        public Restriction restriction { get; set; }
        public RequestOptions requestOptions { get; set; }
    }
}
