using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;
using Thinktecture.IdentityModel.Owin;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Configuration;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using System;

namespace LeaveAPI
{

    public class Startup
    {
        /*Észrevételek:
          A controller classok nevei és a benne lévő függvények határozzák meg a resource-ok path-jeit. (A függvénynevek legelején kell a HTTP method: GET, PUT, POST vagy DELETE)
          Például ha van egy TestController-en belül egy GetTime, akkor http://localhost:0000/Test/Time
          A megadott paraméterek a függvények paramétereiként viselkednek, de ugyanolyan típusnak kell lenniük.
          */
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Minden kimenet JSON
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            //Basic authentication
            appBuilder.UseBasicAuthentication(new BasicAuthenticationOptions("SecureAPI", async (username, password) => await Authenticate (username, password)));
            appBuilder.UseWebApi(config);
        }

        private async Task<IEnumerable<Claim>> Authenticate (string username, string password)
        {
            if (username == password)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("name", username)
                };
                return claims;
            }
            return null;
        }

        public class ScopeAuthorizeAttribute : AuthorizeAttribute
        {
            private readonly string scope;

            public ScopeAuthorizeAttribute(string scope)
            {
                this.scope = scope;
            }

            public override void OnAuthorization(HttpActionContext actionContext)
            {
                base.OnAuthorization(actionContext);

                // Get the Auth0 domain, in order to validate the issuer
                var domain = $"http://{ConfigurationManager.AppSettings["localhost:9000"]}/";

                // Get the claim principal
                ClaimsPrincipal principal = actionContext.ControllerContext.RequestContext.Principal as ClaimsPrincipal;

                // Get the scope claim. Ensure that the issuer is for the correct Auth0 domain
                var scopeClaim = principal?.Claims.FirstOrDefault(c => c.Type == "scope" && c.Issuer == domain);
                if (scopeClaim != null)
                {
                    // Split scopes
                    var scopes = scopeClaim.Value.Split(' ');

                    // Succeed if the scope array contains the required scope
                    if (scopes.Any(s => s == scope))
                        return;
                }

                HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}
