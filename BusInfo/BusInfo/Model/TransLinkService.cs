using System.Net.Http;
using System.Threading.Tasks;

namespace BusInfo.Model
{
    class TransLinkService : ITransLinkService
    {
        private const string baseURL = "http://api.translink.ca/rttiapi/v1/stops/50996/estimates?apikey=13EpwPoHGIACYo3NM5Lh&routeno=15";
        public async Task <NextBus> GetNextBus()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(baseURL);
            var result = new NextBus(response);
            return result;
        }

    }
}
