using System;
using System.Threading.Tasks;
using TransInfo.DataModel;

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
            Console.WriteLine("Next Bus is in {0}", nb.Schedules[0].ExpectedLeaveTime);
            Console.WriteLine("And then at {0}", nb.Schedules[1].ExpectedLeaveTime );
            //Convert the xml response to object of type TransInfo
            Console.Read();

            
            //local changes

        }
    }
}

