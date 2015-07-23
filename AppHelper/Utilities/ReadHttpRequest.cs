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

        public static async void LoadRemoteData(string url)
        {
            HttpClient httpClient = new HttpClient();
            string responseBodyAsText;

            // Limit the max buffer size for the response so we don't get overwhelmed
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");


            try
            {
                responseBodyAsText = await httpClient.GetStringAsync(url);

            }
            catch (HttpRequestException hre)
            {
                responseBodyAsText = hre.ToString();
            }
            catch (Exception ex)
            {
                // For debugging
                responseBodyAsText = ex.ToString();
            }

        }


    }
}