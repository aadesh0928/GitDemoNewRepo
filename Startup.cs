using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using SampleApi.Provider;

[assembly: OwinStartup(typeof(SampleApi.Startup))]
namespace SampleApi
{
    public class Startup
    {
        public void Configure(IAppBuilder app)
        {

            app.UseCors(CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
            {
                Provider = new MySampleOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),
                TokenEndpointPath = new PathString("/token"),
                AllowInsecureHttp = true
            };
            
            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}