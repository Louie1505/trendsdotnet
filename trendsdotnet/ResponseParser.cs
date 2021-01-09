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
        public void Dispose()
        {
            //TODO 
        }

        public async Task<IResponse> Parse(string json, RequestType type)
        {
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
