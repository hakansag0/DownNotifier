using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Utilities.HealthCheck
{
    public class HealthChecker
    {
        public static async Task<bool> CheckURL(string url)
        {
            var handler = new HttpClientHandler();
            //handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            //handler.ServerCertificateCustomValidationCallback =
            //    (httpRequestMessage, cert, cetChain, policyErrors) =>
            //    {
            //        return true;
            //    };
            HttpClient client = new HttpClient(handler);
            //client.BaseAddress = new Uri(url);

            var response = await client.GetAsync(url);
            int statusCode = (int)response.StatusCode;
            return statusCode >= 200 && statusCode < 300;
        }
    }
}
