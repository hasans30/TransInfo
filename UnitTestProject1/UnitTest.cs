using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
       {
        //    Task<string> t1 = TransInfo.UtilMain.Utils.LoadRemoteData("http://api.translink.ca/rttiapi/v1/stops/50996/estimates?apikey=13EpwPoHGIACYo3NM5Lh&routeno=15");
        //    t1.Wait(6000);
        //    string str = t1.Result;
        //    //Console.WriteLine(str);
            /*
            NextBus nb = new NextBus(str);
            Console.WriteLine("Displaying data for {0} {1}", nb.RouteName, nb.Direction);
            Console.WriteLine("Next Bus is in {0}", nb.Schedules[0].ExpectedLeaveTime);
            Console.WriteLine("And then at {0}", nb.Schedules[1].ExpectedLeaveTime);
            */
        }
    }
}
