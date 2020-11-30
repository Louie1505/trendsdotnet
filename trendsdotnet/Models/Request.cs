using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Trendsdotnet.Models.Payloads;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Trendsdotnet.Models
{
    enum RequestType { Explore, RelatedSearches, Multiline, ComparedGeo }
    class Request
    {
        public RequestType Type { get; set; }
        public string Hl { get; set; }
        public string Tz { get; set; }
        public Payload Payload { get; set; }
        public string Token { get; set; }
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
        public Request(RequestType type, string hl, string tz, Payload p)
        {
            this.Type = type;
            this.Hl = hl;
            this.Tz = tz;
            this.Token = RequestData.RequestToken;
            this.Payload = p;
        }
        public async Task<string> Send()
        {
            if (string.IsNullOrEmpty(this.Token))
            {
                //Request with no terms returns 400 so just stick a fake one on
                Models.Payloads.Explore ex = new Models.Payloads.Explore();
                ex.comparisonItem.Add(new ComparisonItem("term", "US"));
                Request req = new Request(RequestType.Explore, "en-US", "0", ex);
                //Don't wanna get stuck in an infinite loop
                req.Token = "Fake Token";
                string res = await req.Send();
                if (string.IsNullOrEmpty(res))
                    throw new Exception("Unable to authenticate. Trends API may be down.");
                //Don't care about this response, just hack out the token
                res = res.Substring(res.IndexOf("\"token\""));
                this.Token = res.Substring(9, res.IndexOf("\",\"id\"") - 9);
            }
            var handler = new HttpClientHandler() { CookieContainer = RequestData.Cookies };
            using HttpClient client = new HttpClient(handler);
            string payload = JsonConvert.SerializeObject(this.Payload);
            string requestUrl = $"{this.URL}?hl={this.Hl}&tz={this.Tz}&req={payload}&token={this.Token}";
            var resp = client.GetAsync(requestUrl).Result;
            //Workaround for too many requests issue
            if (resp != null && resp.Headers.Contains("set-cookie"))
            {
                resp.Headers.TryGetValues("set-cookie", out IEnumerable<string> cookies);
                for (int i = 0; i < cookies.Count(); i++)
                {
                    //TODO - this is horribly inefficient
                    string s = cookies.ElementAt(i).Replace(" ", "");
                    string name = s.Substring(0, s.IndexOf("="));
                    string val = s.Substring(s.IndexOf("="), s.IndexOf(";") - s.IndexOf("="));
                    RequestData.Cookies.Add(new Uri("https://www.google.com"), new Cookie(name, val));
                }
                if (resp.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    return await this.Send();
                }
            }
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsStringAsync();
            }
            else
            {
                //TODO
            }
            return null;
        }
    }
    public static class RequestData
    {
        public static string RequestToken;
        public static CookieContainer Cookies = new CookieContainer();
    }
}
