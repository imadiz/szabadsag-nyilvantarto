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
        public SqlConnection GetSqlConnString()//SQLConnectionString
        {
            return new SqlConnection($@"Server={Environment.MachineName}\SQLEXPRESS;" +
                                       "Trusted_Connection=True;" +
                                       "Database=Szabadsagnyilvantarto;" +
                                       "Connection timeout=10;" +
                                       "Integrated Security=SSPI;");
        }
        // GET api/public/ 
        public JObject GetCurrentTime/*A függvény neve a /-jel után jön, utána ha van, ?paraméter1=érték1&paraméter2=érték2, stb.*/()
        {
            JObject JsonDateTime = new JObject
            {
                { "CurrentDateTimeOffset", DateTimeOffset.Now }
            };
            return JsonDateTime;
        }
        public JObject Login(string username, string password)
        {
            using (SqlConnection MyConn = GetSqlConnString())
            {
                SqlCommand GetUser = new SqlCommand($"SELECT * FROM Users WHERE username = '{username}'", MyConn);//User megkeresése
                MyConn.Open();//Connection megnyitása
                SqlDataReader reader = GetUser.ExecuteReader();//Reader elindítása
                if (reader.HasRows)
                {
                    reader.Close();
                    GetUser = new SqlCommand($"SELECT password FROM Users WHERE username = '{username}' AND password = '{password.GetHashCode()}'", MyConn);
                    reader = GetUser.ExecuteReader();
                    if (reader.HasRows)
                        return new JObject()
                        {
                            { "CallName", "Login" },
                            { "Value", "Success" }
                        };
                }
                else
                {
                    return new JObject()
                    {
                        {"CallName", "Login"},
                        {"Value", "User not found" }
                    };
                }

                //Itt kell nem talált usert/hibás jelszót visszadobni

                reader.Close();//Reader bezárás

            }
        }
    }
}
