using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using GalaSoft.MvvmLight;
namespace BusInfo.Model
{

    public class NextBus : ObservableObject
    {
        public string RouteNo { get; set; }
        public string StopNo { get; set; }
        public string RouteName { get; set; }
        public string Direction { get; set; }
        public string Destination { get; set; }
        public string RouteMapUrl { get; set; }
        public string FirstEstimatedTime { get; set; }
        public string SecondEstimatedTime { get; set; }
        public string ThirdEstimatedTime { get; set; }

        public List<Schedule> Schedules;
        public NextBus()
        {
            Schedules = new List<Schedule>();
        }

        public NextBus(string str, string stopNo)
        {
            this.StopNo = stopNo;
        
            XElement routeNode = XElement.Parse(str);

            if (routeNode != null)
            {
                RouteNo = routeNode.Element("RouteNo").Value;
                RouteName = routeNode.Element("RouteName").Value;
                Direction = routeNode.Element("Direction").Value;
                RouteMapUrl = routeNode.Element("RouteMap").Value;

                IEnumerable<Schedule> xnode1 = from elm in routeNode.Elements("Schedules").Elements("Schedule")
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
                Destination = Schedules[0].Destination;
                FirstEstimatedTime = Schedules[0].ExpectedLeaveTime.ToString("HH:mm");
                SecondEstimatedTime = Schedules[1].ExpectedLeaveTime.ToString("HH:mm");
                ThirdEstimatedTime = Schedules[2].ExpectedLeaveTime.ToString("HH:mm");
            }

        }
    }


    public class Schedule
    {
        public string Destination { get; set; }
        public DateTime ExpectedLeaveTime { get; set; }
        public Int32 ExpectedCountdown { get; set; }
        public string ScheduleStatus { get; set; }
        public Boolean CancelledTrip { get; set; }
        public Boolean CancelledStop { get; set; }
        public Boolean AddedTrip { get; set; }
        public Boolean AddedStop { get; set; }
        public DateTime LastUpdated { get; set; }

    }


    public class NextBuses
    {
        public Int64 StopID;
        List<String> RouteNos { get; set; }
        private List<NextBus> _buses;
        public List<NextBus> Buses
        {
            get;
            set;
        }

        public NextBuses()
        {
            Buses = new List<NextBus>();
            RouteNos = new List<String>();
            StopID = 0;
        }


        public NextBuses(string xml, int stopID)
        {
            Buses = new List<NextBus>();
            RouteNos = new List<String>();
            XElement xdoc = XElement.Parse(xml);
            var routeNodes = (from elm in xdoc.Elements("NextBus")
                             select elm);

            foreach(var routeNode in routeNodes)
            {
                NextBus nb = new NextBus(routeNode.ToString(), Convert.ToString(stopID));
                Buses.Add(nb);
                RouteNos.Add(nb.RouteNo);
            }

            this.StopID = stopID;

        }


    }
}
