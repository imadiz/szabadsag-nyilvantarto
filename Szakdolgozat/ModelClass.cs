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

namespace Szakdolgozat
{
    public partial class ModelClass : ObservableObject
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
        public async void StartNetworkTime()
        {
            HttpClient client = new();

            //Task-ok használatánál csak az await kulcsszót fogadja el várakozásra, ha van .Wait() vagy .Result ott megáll a kód hiba nélkül.

            while (true)
            {
                string json_response = "";

                try
                {
                    json_response = await client.GetStringAsync("https://worldtimeapi.org/api/timezone/Europe/Budapest");
                    JsonNode rootnode = JsonNode.Parse(json_response)!;
                    CurrentTime = (DateTimeOffset)rootnode["datetime"]!;
                }
                catch (HttpRequestException re_ex)
                {
                    CurrentTime = DateTimeOffset.Now;
                }
                await Task.Delay(500);
            }            
        }

        public ModelClass()
        {
            Task.Run(StartNetworkTime);

            /*Szóval az a terv, hogy minden hónap első napjától elmegyek visszafele, amíg nem találok egy hétfőt.
              Utána elindulok előrefele, addig amíg a hónap végéig nem érek, így van egy 2D-s tömböm ami hétfőtől kezdődik minden hónapra, így a megjelenítés megvan.*/

            //for (int i = 1; i <= 12; i++)
            //{
            //    MonthDisplay CurrentDisplay = new(DateTimeFormatInfo.InvariantInfo.GetMonthName(i), new ObservableCollection<DateTimeOffset>());//Jelen megjelenítő

            //    DateTime date = new DateTime(DateTime.Now.Year, i, 1);//Idén, jelen hónap elseje

            //    while (date.DayOfWeek != DayOfWeek.Monday)
            //        date = date.AddDays(-1);

            //    bool EnteredMonth = false;

            //    while (EnteredMonth && //Ha már belépett a hónapba
            //           !date.Month.Equals(new DateTime(DateTime.Now.Year, i, 1)))//Ha már nincs benne a jelenlegi hónapban
            //    {
            //        CurrentDisplay.DisplayDates.Add(date);
            //        date = date.AddDays(1);

            //        if (date.Month.Equals(i))
            //            EnteredMonth = true;
            //    }

            //    AllMonths.Add(CurrentDisplay);
            //}
        }
    }
}