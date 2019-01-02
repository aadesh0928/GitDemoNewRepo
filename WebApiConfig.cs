using Microsoft.Owin.Security.OAuth;
using SampleApi.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SampleApi
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();


            var corsAttribute = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*", exposedHeaders : "*");

            httpConfiguration.EnableCors(corsAttribute);


            httpConfiguration.MessageHandlers.Add(new MyMessageHandler());
            httpConfiguration.MessageHandlers.Add(new MyMessageHandler2());

           // httpConfiguration.EnableSystemDiagnosticsTracing();

            //httpConfiguration.SuppressDefaultHostAuthentication();
           
            httpConfiguration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            httpConfiguration.MapHttpAttributeRoutes();

            //httpConfiguration.Routes.MapHttpRoute(name: "DefaultRoute",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });

            httpConfiguration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();



            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


           

            return httpConfiguration;
        }
    }
}