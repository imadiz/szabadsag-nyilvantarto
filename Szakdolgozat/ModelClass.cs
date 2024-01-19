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
        public DateTimeOffset GetNetworkTime()
        {
            HttpClient client = new();

            Task<string> response = client.GetStringAsync("https://worldtimeapi.org/api/timezone/Europe/Budapest");
            string json_response = response.Result;
            JsonNode rootnode = JsonNode.Parse(json_response)!;

            return (DateTimeOffset)rootnode["datetime"]!;
        }

    }
}
