using System.Net.Http.Formatting;
using System.Web.Http;

namespace WebApi.Jwt
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
               .SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
