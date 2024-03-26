using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAPI
{
    public class Program
    {
        static async Task Main()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpClient and make a request to api/values 
                HttpClient client = new HttpClient();

                byte[] authdata = new UTF8Encoding().GetBytes("LeaveClient:Jb4p&$DqL9TwVBrW5TUjay284iJsA^^a");/*Tech. Felhasználónév:Jelszó*/
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authdata));//Auth adatok request-hez adása

                HttpResponseMessage response = await client.GetAsync(baseAddress + "api/private/CurrentTime");
                

                Console.WriteLine(response);

                Console.WriteLine("Call válasza:\n" + JObject.Parse(await response.Content.ReadAsStringAsync()));


                Console.ReadLine();
            }
        }
    }
}