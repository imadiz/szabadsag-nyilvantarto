using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace LeaveAPI
{
    public class PublicController : ApiController
    {
        public JObject GetCurrentTime/*A függvény neve a /-jel után jön, utána ha van, ?paraméter1=érték1&paraméter2=érték2, stb.*/()
        {
            JObject JsonDateTime = new JObject
            {
                { "CurrentDateTimeOffset", DateTimeOffset.Now }
            };
            return JsonDateTime;
        }
        public JObject GetLogin(string username, string password)
        {
            using (SqlConnection MyConn = new SqlConnection($@"Server={Environment.MachineName}\SQLEXPRESS;" +
                                                              "Trusted_Connection=True;" +
                                                              "Database=Szabadsagnyilvantarto;" +
                                                              "Connection timeout=10;" +
                                                              "Integrated Security=SSPI;"))
            {
                SqlCommand GetUser = new SqlCommand($"SELECT * FROM Users WHERE username = '{username}'", MyConn);//Username megkeresése
                MyConn.Open();//Connection megnyitása
                SqlDataReader reader = GetUser.ExecuteReader();//Reader elindítása
                if (reader.HasRows)//Ha van ilyen username a DB-ben
                {//Van user
                    reader.Close();//Reader bezár
                    GetUser = new SqlCommand($"SELECT password FROM Users WHERE username = '{username}' AND password = '{password.GetHashCode()}'", MyConn);//Username+Password kombó keresése
                    reader = GetUser.ExecuteReader();

                    bool FoundLoginData = reader.HasRows;//Találat
                    reader.Close();//Reader bezárás

                    if (FoundLoginData)//Ha jó a login kombó
                        return new JObject()//Bejelentkezés
                        {
                            { "CallName", "Login" },
                            { "Value", "Success" }
                        };
                    else
                        return new JObject()//Rossz jelszó
                        {
                            { "CallName", "Login" },
                            { "Value", "Wrong password" }
                        };
                }
                else
                {//Nincs user
                    return new JObject()
                    {
                        { "CallName", "Login"},
                        { "Value", "User not found" }
                    };
                }
            }
        }
    }
}