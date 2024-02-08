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
        }
    }
}
