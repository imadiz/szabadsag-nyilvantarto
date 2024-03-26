using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Security.Cryptography;

namespace Szakdolgozat.Models
{
    public partial class LoginModelClass : ObservableObject
    {
        [ObservableProperty]
        private SolidColorBrush _usernameBoxBorderColor = new(Colors.DarkGray);

        [ObservableProperty]
        private SolidColorBrush _passwordBoxBorderColor = new(Colors.DarkGray);

        [ObservableProperty]
        private string _errorChipText = "";

        [ObservableProperty]
        private bool _errorChipIsVisible = false;

        [ObservableProperty]
        private string _username = "";

        [ObservableProperty]
        private string _password = "";

        public async Task<JObject> SendPostAPICall(string CallName, JObject CallContent)
        {
            HttpClient client = new();

            byte[] authdata = new UTF8Encoding().GetBytes("LeaveClient:Jb4p&$DqL9TwVBrW5TUjay284iJsA^^a");/*Tech. Felhasználónév:Jelszó*/
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authdata));//Auth adatok request-hez adása

            HttpResponseMessage response = await client.PostAsJsonAsync($"http://localhost:9000/api/private/{CallName}", CallContent);//Call elküldése

            return JObject.Parse(await response.Content.ReadAsStringAsync());//Call válasz
        }

        public static string CreateSHA256(string input)
        {
            // Use input string to calculate MD5 hash
            using (SHA256 sha = SHA256.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); //
            }
        }


        public async void AttemptLogin()
        {
            JObject LoginData = new JObject()
            {
                { "username", Username },
                { "password", CreateSHA256(Password) }
            };//Felhasználói adatok

            JObject response = await SendPostAPICall("UserLogin", LoginData);//Call elküldése a loginadatokkal a body-ba

            switch (response["LoginAttempt"].Value<string>())//Call resp. érték
            {
                case "Success"://Siker
                    ErrorChipIsVisible = false;//Nincs hiba

                    MessageBus.Current.SendMessage("", "ChangeView");//Beléptetés
                    break;
                case "Bad password"://Rossz jelszó
                    ErrorChipText = "Hibás jelszó!";//Hiba szöveg
                    ErrorChipIsVisible = true;//Van hiba

                    PasswordBoxBorderColor = new(Colors.Red);//Jelszó keret színváltoztatás
                    break;
                case "User not found"://Nincs ilyen user
                    ErrorChipText = "Nincs ilyen felhasználó!";//Hiba szöveg
                    ErrorChipIsVisible = true;//Van hiba

                    UsernameBoxBorderColor = new(Colors.Red);//Felhasználónév keret színváltoztatás
                    break;
                default:
                    ErrorChipText = "Nem kezelt hiba lépett fel.";//Hiba szöveg
                    ErrorChipIsVisible = true;//Van hiba
                    break;
            }
        }
    }
}
