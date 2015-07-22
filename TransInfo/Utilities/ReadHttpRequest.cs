using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TransInfo.Util
{
    public class Utils
    {

        public static async Task<string> LoadRemoteData(string url)
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 1 * 20;
            HttpResponseMessage response = await client.GetAsync(
                 new Uri(url));

            string responseText = await response.Content.ReadAsStringAsync();
            return responseText;
        }


    }
}