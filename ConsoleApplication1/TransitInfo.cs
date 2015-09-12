using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace TransInfo.DataModel
{
    class NextBus
    {
        public string RouteNo { get; set; }
        public string RouteName { get; set; }
        public string Direction { get; set; }
        public string RouteMapUrl { get; set; }

        public List<Schedule> Schedules;

        public NextBus()
        {
            Schedules = new List<Schedule>();
        }

        public NextBus(string str)
        {
            XElement xdoc = XElement.Parse(str);

            var routeNode = (from elm in xdoc.Elements("NextBus")
                             select elm).SingleOrDefault();

            if (routeNode != null)
            {
                RouteNo = routeNode.Element("RouteNo").Value;
                RouteName = routeNode.Element("RouteName").Value;
                Direction = routeNode.Element("Direction").Value;
                RouteMapUrl = routeNode.Element("RouteMap").Value;



                IEnumerable<Schedule> xnode1 = from elm in xdoc.Elements("NextBus").Elements("Schedules").Elements("Schedule")
                                               select new Schedule
                                               {
                                                   Destination = elm.Element("Destination").Value,
                                                   ExpectedLeaveTime = Convert.ToDateTime(elm.Element("ExpectedLeaveTime").Value),
                                                   ExpectedCountdown = Convert.ToInt32(elm.Element("ExpectedCountdown").Value),
                                                   ScheduleStatus = elm.Element("ScheduleStatus").Value,
                                                   CancelledTrip = Convert.ToBoolean(elm.Element("CancelledTrip").Value),
                                                   CancelledStop = Convert.ToBoolean(elm.Element("CancelledStop").Value),
                                                   AddedTrip = Convert.ToBoolean(elm.Element("AddedTrip").Value),
                                                   AddedStop = Convert.ToBoolean(elm.Element("AddedStop").Value),
                                                   LastUpdated = Convert.ToDateTime(elm.Element("LastUpdate").Value)
                                               };

                Schedules = xnode1.ToList();
            }

        }
    }


    class Schedule
    {
        public string Destination { get; set; }
        public DateTime ExpectedLeaveTime { get; set; }
        public Int32 ExpectedCountdown { get; set; }
        public string ScheduleStatus { get; set; }
        public Boolean  CancelledTrip { get; set; }
        public Boolean CancelledStop { get; set; }
        public Boolean AddedTrip { get; set; }
        public Boolean AddedStop { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
