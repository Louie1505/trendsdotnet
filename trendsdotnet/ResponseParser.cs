using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trendsdotnet.Models.Responses;
using Trendsdotnet.Models;

namespace Trendsdotnet
{
    interface IResponse { }
    class ResponseParser : IDisposable
    {
        /// <summary>
        /// Need to know what the start of the JSON is, depends what request type we're parsing
        /// </summary>
        private Dictionary<RequestType, string> reqTypeJsonMap = new Dictionary<RequestType, string>
        {
            { RequestType.Explore, "\"widgets\"" },
            { RequestType.Multiline, "\"default\"" }
        };

        public void Dispose()
        {
            //TODO 
        }

        public async Task<IResponse> Parse(string json, RequestType type)
        {
            //The json has a load of shit on the start for some reason so just strip it off
            json = json.Substring(json.IndexOf(reqTypeJsonMap[type]) - 1);
            switch (type)
            {
                case RequestType.Explore:
                    break;
                case RequestType.RelatedSearches:
                    break;
                case RequestType.Multiline:
                    JObject jobj = JObject.Parse(json);
                    return jobj["default"].ToObject<TimelineData>();
                case RequestType.ComparedGeo:
                    break;
                default:
                    return null;
            }
            return null;
        }

    }
}
