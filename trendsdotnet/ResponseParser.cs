using Newtonsoft.Json.Linq;
using System;
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
            JObject jobj;
            switch (type)
            {
                case RequestType.Explore:
                    break;
                case RequestType.RelatedSearches:
                    break;
                case RequestType.Multiline:
                    jobj = JObject.Parse(json);
                    return jobj["default"].ToObject<InterestTimeline>();
                case RequestType.ComparedGeo:
                    jobj = JObject.Parse(json);
                    return jobj["default"].ToObject<RegionInterestMap>();
                default:
                    return null;
            }
            return null;
        }

    }
}
