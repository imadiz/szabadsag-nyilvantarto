using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Classes;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Text.Json.Nodes;
using System.Globalization;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Szakdolgozat
{
    public partial class LeaveModelClass : ObservableObject
    {
        //Adatok és logika
        [ObservableProperty]
        private DateTimeOffset _currentyear = new(DateTime.Now);//Az évszámválasztó értéke

        [ObservableProperty]
        private ObservableCollection<LeaveType> _leavetypes = new();

        [ObservableProperty]
        private DateTimeOffset _currentTime = new();

        [ObservableProperty]
        private string _debugText = "";

        [ObservableProperty]
        private ObservableCollection<MonthDisplay> _allMonths = new();
        public async Task StartNetworkTime()
        {
            HttpClient client = new();

            //Task-ok használatánál csak az await kulcsszót fogadja el várakozásra, ha van .Wait() vagy .Result ott megáll a kód hiba nélkül.

            while (true)
            {
                try
                {
                    DebugText = SendGetAPICall("public/CurrentTime").Result["Value"].Value<string>();
                }
                catch (HttpRequestException re_ex)//Ha nem sikerül lekérni az időt
                {
                    CurrentTime = new DateTimeOffset(new DateTime(1970, 1, 1));//Alap idő
                }
                await Task.Delay(500);
            }            
        }
        public void CreateCalendarDisplay()
        {
            /*Szóval az a terv, hogy minden hónap első napjától elmegyek visszafele, amíg nem találok egy hétfőt.
              Utána elindulok előrefele, addig amíg a hónap végéig nem érek, így van egy 2D-s tömböm ami hétfőtől kezdődik minden hónapra, így a megjelenítés megvan.*/
            for (int i = 1; i <= 12; i++)
            {
                MonthDisplay CurrentDisplay = new(string.Concat(char.ToUpper(DateTimeFormatInfo.GetInstance(new CultureInfo("hu-HU")).MonthNames[i - 1][0]),//Első karakter nagybetű
                                                                DateTimeFormatInfo.GetInstance(new CultureInfo("hu-HU")).MonthNames[i - 1][1..]),//Többi kicsi
                                                  new ObservableCollection<DateTimeOffset>());//Dátumok listája

                DateTime date = new DateTime(DateTime.Now.Year, i, 1);//Idén, jelen hónap elseje

                while (date.DayOfWeek != DayOfWeek.Monday)
                    date = date.AddDays(-1);

                bool EnteredMonth = false;

                while (true)
                {
                    CurrentDisplay.DisplayDates.Add(date);
                    date = date.AddDays(1);

                    if (date.Month.Equals(i))
                        EnteredMonth = true;

                    if (EnteredMonth && !date.Month.Equals(i))//Ha már belépett a hónapba, és ki is lépett
                        break;
                }

                AllMonths.Add(CurrentDisplay);
            }
        }

        public async Task<JObject> SendGetAPICall(string CallName)
        {
            HttpClient client = new();

            byte[] authdata = new UTF8Encoding().GetBytes("LeaveClient:Jb4p&$DqL9TwVBrW5TUjay284iJsA^^a");/*Tech. Felhasználónév:Jelszó*/
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authdata));//Auth adatok request-hez adása

            HttpResponseMessage response = await client.GetAsync($"http://localhost:9000/api/{CallName}");//Call elküldése

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        public async Task<JObject> SendPostAPICall(string CallName, JObject CallContent)
        {
            HttpClient client = new();

            byte[] authdata = new UTF8Encoding().GetBytes("LeaveClient:Jb4p&$DqL9TwVBrW5TUjay284iJsA^^a");/*Tech. Felhasználónév:Jelszó*/
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authdata));//Auth adatok request-hez adása

            HttpResponseMessage response = await client.PostAsync($"http://localhost:9000/api/{CallName}", new StringContent(CallContent.ToString()));//Call elküldése

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public LeaveModelClass()
        {
            CreateCalendarDisplay();
        }
    }
}