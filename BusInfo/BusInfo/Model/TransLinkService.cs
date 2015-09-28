using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusInfo.Model
{
    class TranslinkService : ITranslinkService
    {
        private const string baseURL = "http://api.translink.ca/rttiapi/v1/stops/{0}/estimates?apikey=13EpwPoHGIACYo3NM5Lh";
        public async Task <NextBuses> GetNextBus()
        {
            throw new NotImplementedException();
        }

        public async Task<NextBuses> GetNextBus(int stopID)
        {
            HttpClient httpClient = new HttpClient();
            string url = string.Format(baseURL, stopID);
            Uri actualUri = new Uri(url);
            var response = await httpClient.GetStringAsync(actualUri);
            var result = new NextBuses(response, stopID);
            return result;

        }

        /// <summary>
        /// This will filter by Bus No for a given stop
        /// </summary>
        /// <param name="stopID"></param>
        /// <param name="busNo"></param>
        /// <returns></returns>
        public async Task<NextBuses> GetNextBus(int stopID, int busNo)
        {

            try
            {
                HttpClient httpClient = new HttpClient();
                string url = string.Format(baseURL+ "&routeno={1}", stopID, busNo);
                Uri actualUri = new Uri(url);
                var response = await httpClient.GetStringAsync(actualUri);
                var result = new NextBuses(response,stopID);
                return result;
            }
            catch(Exception ex)
            {
                NextBuses nbs = new NextBuses();
                //bad error handling. need to fix.
                nbs.Buses.Add( new NextBus { RouteNo = "17", RouteName = ex.Message, Schedules = { new Schedule { Destination = "dst", ExpectedLeaveTime = DateTime.Now }, new Schedule { Destination = "dst", ExpectedLeaveTime = DateTime.Now } } });
                return nbs;
            }
        }



    }
}
