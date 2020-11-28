using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using trendsdotnet.Models.Payloads;

namespace trendsdotnet.Models
{
    enum RequestType { Explore, RelatedSearches, Multiline, ComparedGeo }
    class Request
    {
        private string URL
        {
            get
            {
                switch (this.Type)
                {
                    case RequestType.Explore:
                        return "https://trends.google.com/trends/api/explore";
                    case RequestType.RelatedSearches:
                        return "https://trends.google.com/trends/api/widgetdata/relatedsearches";
                    case RequestType.Multiline:
                        return "https://trends.google.com/trends/api/widgetdata/multiline";
                    case RequestType.ComparedGeo:
                        return "https://trends.google.com/trends/api/widgetdata/comparedgeo ";
                    default:
                        return "";
                }
            }
        }

        public RequestType Type { get; set; }
        public string Hl { get; set; }
        public string Tz { get; set; }
        public Payload Payload { get; set; }
        public string Token { get; set; }
        public async Task<Response> Send()
        {
            using (HttpClient client = new HttpClient())
            {
                string payload = JsonConvert.SerializeObject(this.Payload);
                string requestUrl = $"{this.URL}?hl={this.Hl}&tz={this.Tz}&req={payload}&token={this.Token}";
                var resp = await client.GetAsync(requestUrl);
                if (resp.IsSuccessStatusCode)
                {
                    using (ResponseParser Parser = new ResponseParser())
                    {
                        string content = await resp.Content.ReadAsStringAsync();
                        return await Parser.Parse(content);
                    }
                }
                else
                {
                    if (resp.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        //TODO
                    }
                    //TODO
                }
                return null;
            }
        }
    }
}
