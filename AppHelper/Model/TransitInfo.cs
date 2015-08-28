using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
namespace TransInfo.DataModel
{
    class NextBus
    {
        public string RouteNo { get; set; }
        public string RouteName { get; set; }
        public string Direction { get; set; }
        public string RouteMapUrl { get; set; }

        public List<Schedule> Schedules;

        public NextBus(string routeNo, string routeName, string direction, string routeMapUrl)
        {
            RouteNo = routeNo;
            RouteName = routeName;
            Direction = direction;
            RouteMapUrl = routeMapUrl;
            Schedules = new List<Schedule>();

        }

        public NextBus(string str)
        {
            XElement xdoc = XElement.Parse(str);
            foreach(var xnode in xdoc.Descendants("NextBuses / NextBus / Schedules / Schedule"))
            {
                foreach( var el in xnode.Elements())
                {
                    //to do code
                }
                              
            }


        }

        public void SetSchedule(List<Schedule> schdl)
        {
            Schedules = schdl;
        }
    }

    class Schedule
    {
        public string Destinition { get; set; }
        public DateTime ExpectedLeaveTime { get; set; }
        public Int32 ExpectedCountdown { get; set; }
        public string ScheduleStatus { get; set; }
        public Boolean  CancelledTrip { get; set; }
        public Boolean CancelledStop { get; set; }
        public Boolean AddedTrip { get; set; }
        public Boolean AddedStop { get; set; }
        public DateTime LastUpdated { get; set; }

        public Schedule(string destination, DateTime expectedLeaveTime, Int32 expectedCountdown, string scheduleStatus,
            Boolean cancelledTrip, Boolean cancelledStop, Boolean addedTrip, Boolean addedStop, DateTime lastUpdated)
        {
            Destinition = destination;
            ExpectedLeaveTime = expectedLeaveTime;
            ExpectedCountdown = expectedCountdown;
            ScheduleStatus = scheduleStatus;
            CancelledTrip = cancelledTrip;
            CancelledStop = cancelledStop;
            AddedTrip = addedTrip;
            AddedStop = addedStop;
            LastUpdated = lastUpdated;
        }

    }
}
