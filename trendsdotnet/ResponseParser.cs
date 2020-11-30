using System;
using System.Threading.Tasks;
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

        public async Task<IResponse> Parse(string json)
        {
            //The json has a load of shit on the start for some reason so just strip it off
            json = json.Substring(json.IndexOf("\"widgets\"") - 1);

            return null;
        }

    }
}
