using BusInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusInfo.Design
{
    class DesignTranslinkInfoService : ITranslinkService
    {
        public void CacheUserPreference(string strResponse)
        {
            throw new NotImplementedException();
        }

        public void ClearUserPreferenceCache()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCachedUserPreference()
        {
            throw new NotImplementedException();
        }

        public Task<NextBuses> GetNextBus()
        {
            throw new NotImplementedException();
        }

        public Task<NextBuses> GetNextBus(int stopId)
        {
            throw new NotImplementedException();
        }

        public Task<NextBuses> GetNextBus(int stopNo, int busNo)
        {
            NextBus bs = new NextBus {
                RouteNo = busNo.ToString(),
                RouteName = "Route Name:" + stopNo.ToString(),
                Direction = "WEST",
                Destination = "DOWNTOWN",
                FirstEstimatedTime = DateTime.Now.ToString("HH:mm"),
                SecondEstimatedTime = DateTime.Now.ToString("HH:mm"),
                ThirdEstimatedTime = DateTime.Now.ToString("HH:mm"),

                Schedules = { new Schedule { Destination = "dst", ExpectedLeaveTime = DateTime.Now },
                    new Schedule { Destination = "dst", ExpectedLeaveTime = DateTime.Now } }
            };
            NextBuses nbs = new NextBuses();
            nbs.Buses.Add(bs);
            return Task.FromResult(nbs);
        }

    }
}
