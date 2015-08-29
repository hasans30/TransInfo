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
            //Console.WriteLine(str);

            NextBus nb = new NextBus(str);
            Console.WriteLine("Displaying data for {0} {1}", nb.RouteName, nb.Direction);
            Console.WriteLine("Next Bus is at {0}", nb.Schedules[0].ExpectedLeaveTime);
            Console.WriteLine("And then at {0}", nb.Schedules[1].ExpectedLeaveTime);
            //Convert the xml response to object of type TransInfo
            Console.Read();
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

            Schedules=xnode1.ToList();
        }

    }
}

class Schedule
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
    /*
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
        */

}
