using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusInfo.Model
{
    class TransLinkService : ITransLinkService
    {
        private const string baseURL = "http://api.translink.ca/rttiapi/v1/stops/{0}/estimates?apikey=13EpwPoHGIACYo3NM5Lh&routeno=17";
        public async Task <NextBus> GetNextBus()
        {
            return await GetNextBus(50996);
        }

        public async Task<NextBus> GetNextBus(int stopNo)
        {
            HttpClient httpClient = new HttpClient();
            Uri actualUri  = new Uri(string.Format(baseURL,
                                                        stopNo));
            var response = await httpClient.GetStringAsync(actualUri);
            var result = new NextBus(response);
            return result;
        }

    }
}
