using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Trendsdotnet.Models;
using Trendsdotnet.Models.Responses;

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
                    jobj = JObject.Parse(json);
                    return jobj["default"]["rankedList"][0].ToObject<RankedQueryList>();
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
