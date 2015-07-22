using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        //some comment
        static void Main(string[] args)
        {
            Task<string> response =  TransInfo.Util.Utils.LoadRemoteData("http://api.translink.ca/rttiapi/v1/stops/50996/estimates?apikey=13EpwPoHGIACYo3NM5Lh&routeno=15");
            Console.WriteLine("2 waiting for task to finish...");
            string str = response.Result;
            Console.WriteLine(str);
            Console.ReadLine();
            //local changes

        }
    }
}
