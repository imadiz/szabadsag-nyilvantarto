using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LeaveAPI
{
    [Authorize]
    public class PrivateController : ApiController
    {
        [HttpBindNever]
        public SqlConnection DBConn { get; } = new SqlConnection($@"Server={Environment.MachineName}\SQLEXPRESS;" +
                                       "Trusted_Connection=True;" +
                                       "Database=Szabadsagnyilvantarto;" +
                                       "Connection timeout=10;" +
                                       "Integrated Security=SSPI;");//SQLConnectionString

        [HttpGet]
        public JObject CurrentTime/*A függvény neve a /-jel után jön, utána ha van, ?paraméter1=érték1&paraméter2=érték2, stb.*/()
        {
            return new JObject
            {
                { "CallName", MethodBase.GetCurrentMethod().Name },
                { "Value", DateTimeOffset.Now }
            };
        }
        [HttpPost]
        public JObject UserLogin([FromBody]JObject LoginData)
        {
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} request érkezett.");
            
            string username = LoginData["username"].Value<string>();
            string password = LoginData["password"].Value<string>();

            using (SqlConnection MyConn = new SqlConnection($@"Server={Environment.MachineName}\SQLEXPRESS;" +
                                                              "Trusted_Connection=True;" +
                                                              "Database=Szabadsagnyilvantarto;" +
                                                              "Connection timeout=10;" +
                                                              "Integrated Security=SSPI;"))
            {
                SqlCommand GetUser = new SqlCommand($"SELECT * FROM Users WHERE username = '{username}'", MyConn);//Username megkeresése
                MyConn.Open();//Connection megnyitása
                SqlDataReader reader = GetUser.ExecuteReader();//Reader elindítása

                JObject response = new JObject()
                {
                    { "CallName", MethodBase.GetCurrentMethod().Name }
                };//Válasz előkészítése

                if (reader.HasRows)//Ha van ilyen username a DB-ben
                {//Van user
                    reader.Close();//Reader bezár
                    GetUser = new SqlCommand($"SELECT password FROM Users WHERE username = '{username}' AND password = '{password}'", MyConn);//Username+Password kombó keresése
                    reader = GetUser.ExecuteReader();

                    bool FoundLoginData = reader.HasRows;//Találat
                    reader.Close();//Reader bezárás

                    if (FoundLoginData)//Ha jó a login kombó
                        response.Add("LoginAttempt", "Success");//Bejelentkezés
                    else
                        response.Add("LoginAttempt", "Bad password");//Rossz jelszó
                }
                else
                    response.Add("LoginAttempt", "User not found");//Nincs user
                Console.WriteLine($"Válasz:\n{response}");
                return response;
            }
        }
    }
}