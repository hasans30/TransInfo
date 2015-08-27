using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace ConsoleApplication1
{
    class Program
    {
        //some comment
        static void Main(string[] args)
        {
            Task<string> t1 = TransInfo.UtilMain.Utils.LoadRemoteData("http://api.translink.ca/rttiapi/v1/stops/50996/estimates?apikey=13EpwPoHGIACYo3NM5Lh&routeno=15");
            t1.Wait(6000);
            string str = t1.Result;
            Console.WriteLine(str);
            NextBus nb = new NextBus(str);

            //Convert the xml response to object of type TransInfo
            

            Console.ReadLine();
            //local changes

        }
    }
}



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

        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(str);
        XmlNode node = xdoc.SelectNodes("NextBuses").Item(0);
        node = node.SelectNodes("NextBus").Item(0);
        //NextBus nb = new NextBus(node.SelectNodes("RouteNo"));
        string routeNo = node.SelectNodes("RouteNo").Item(0).InnerText;
        string routename = node.SelectNodes("RouteName").Item(0).InnerText;
        string direction = node.SelectNodes("Direction").Item(0).InnerText;
        string routeMapUrl = node.SelectNodes("RouteMap").Item(0).InnerText;

        RouteNo = routeNo;
        RouteName = routename;
        Direction = direction;
        RouteMapUrl = routeMapUrl;
        Schedules = new List<Schedule>();

        XmlNodeList xnode1 = xdoc.SelectNodes("NextBuses/NextBus/Schedules/Schedule");

        foreach (XmlNode xnode in xnode1)
        {
            Console.WriteLine("{0}\n{1}",
                xnode.SelectNodes("Pattern").Item(0).InnerText,
                xnode.SelectNodes("ExpectedLeaveTime").Item(0).InnerText
                );

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
        public Boolean CancelledTrip { get; set; }
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
