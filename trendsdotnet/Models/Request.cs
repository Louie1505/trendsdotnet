using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Trendsdotnet.Models.Payloads;

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
        public string RequestUrl { get; set; }
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
                        return "https://trends.google.com/trends/api/widgetdata/comparedgeo";
                    default:
                        return "";
                }
            }
        }
        public Request(RequestType type, string hl, string tz, Payload p, string token)
        {
            this.Type = type;
            this.Hl = hl;
            this.Tz = tz;
            this.Token = token;
            this.Payload = p;
        }
        public async Task<string> Send()
        {
            if (string.IsNullOrEmpty(this.Token))
                throw new Exception("No Auth Token");
            var handler = new HttpClientHandler() { CookieContainer = RequestData.Cookies };
            using HttpClient client = new HttpClient(handler);
            string payload = JsonConvert.SerializeObject(this.Payload);
            this.RequestUrl ??= $"{this.URL}?hl={this.Hl}&tz={this.Tz}&req={payload}&token={this.Token}";
            var resp = client.GetAsync(this.RequestUrl).Result;
            //Workaround for too many requests issue
            if (resp != null && resp.Headers.Contains("set-cookie"))
            {
                resp.Headers.TryGetValues("set-cookie", out IEnumerable<string> cookies);
                for (int i = 0; i < cookies.Count(); i++)
                {
                    //TODO - this is horribly inefficient
                    string s = cookies.ElementAt(i).Replace(" ", "");
                    string name = s.Substring(0, s.IndexOf("="));
                    string val = s[s.IndexOf("=")..s.IndexOf(";")];
                    RequestData.Cookies.Add(new Uri("https://www.google.com"), new Cookie(name, val));
                }
                if (resp.StatusCode == HttpStatusCode.TooManyRequests)
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

        public static async Task<string> GetTokenForRequest(string[] terms, RequestType type)
        {
            //The, I shit you not, actual intended way to get a token is to make an explore request first 
            //and use the nth token based on request type in your actual request...
            ComparisonItem[] comparisonItems = new ComparisonItem[terms.Length];
            for (int i = 0; i < terms.Length; i++)
            {
                ComparisonItem item = new ComparisonItem(terms[i]);
                //I have no idea why I have to do this
                if (type == RequestType.ComparedGeo)
                    item.time = "all";

                comparisonItems[i] = item;
            }
            Request req = new Request(RequestType.Explore, "en-US", "0", null, "Fake token please don't throw an exception because I have no token I do it's this string are you happy now");
            //Can't be fucked writing code for this, but the comparison items HAVE TO be the same as the following request or you get a 401. This API was written by monkeys 🐒
            req.RequestUrl = $"https://trends.google.com/trends/api/explore?hl=en-US&tz=0&req={{\"comparisonItem\":{JsonConvert.SerializeObject(comparisonItems)},\"category\":0,\"property\":\"\"}}&tz=0";
            string res = await req.Send();

            //Remove irrelevant tokens
            switch (type)
            {
                //Multiline uses the first so do nothing
                case RequestType.Multiline:
                    break;
                //2nd token so strip off the first
                case RequestType.ComparedGeo:
                    res = res?.Substring(res.IndexOf("\"token\"") + 55);
                    break;
                case RequestType.RelatedSearches:
                    break;
            }

            //Don't care about the response, just hack out the first token which should now be the one we want.
            res = res?.Substring(res.IndexOf("\"token\""));
            string token = res?.Substring(9, res.IndexOf("\",\"id\"") - 9);
            if (string.IsNullOrEmpty(token))
                throw new Exception("Unable to authenticate. Trends API may be down.");
            return token;
        }
    }
    public static class RequestData
    {
        public static CookieContainer Cookies = new CookieContainer();
    }
}
