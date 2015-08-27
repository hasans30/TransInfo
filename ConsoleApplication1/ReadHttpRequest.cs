using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TransInfo.UtilMain
{
    public class Utils
    {

        public static async Task<string> LoadRemoteData(string url)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            return response;
        }


    }
}