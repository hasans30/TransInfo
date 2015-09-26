using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusInfo.Model
{
    class TranslinkService : ITranslinkService
    {
        private const string baseURL = "http://api.translink.ca/rttiapi/v1/stops/{0}/estimates?apikey=13EpwPoHGIACYo3NM5Lh&routeno={1}";
        public async Task <NextBus> GetNextBus()
        {
            return await GetNextBus(50996);
        }

        public async Task<NextBus> GetNextBus(int stopNo)
        {
            return await GetNextBus(stopNo,17);
        }

        public async Task<NextBus> GetNextBus(int stopNo, int busNo)
        {

            try
            {
                HttpClient httpClient = new HttpClient();
                string url = string.Format(baseURL, stopNo, busNo);
                Uri actualUri = new Uri(url);
                var response = await httpClient.GetStringAsync(actualUri);
                var result = new NextBus(response);
                return result;
            }
            catch(Exception ex)
            {
                //bad error handling. need to fix.
                return new NextBus { RouteNo = "17", RouteName = ex.Message, Schedules = { new Schedule { Destination = "dst", ExpectedLeaveTime = DateTime.Now }, new Schedule { Destination = "dst", ExpectedLeaveTime = DateTime.Now } } };
            }
        }



    }
}
