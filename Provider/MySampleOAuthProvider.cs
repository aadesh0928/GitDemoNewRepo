using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SampleApi.Provider
{
    public class MySampleOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override  Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origion", new[] { "*" });

            if(context.UserName.Equals("aadesh.yadav") && context.Password.Equals("Password"))
            {
                IEnumerable<Claim> claims = new List<Claim>
                {
                    new Claim("Firstname", "aadesh"),
                    new Claim("Lastname", "yadav"),
                    new Claim("LastLoginDate",DateTime.Now.ToShortDateString())
                };
                identity.AddClaims(claims);

                var authProperties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {"username", context.UserName},
                    {"role", "Admin" },
                    {"test_prop","test_value" }
                    
                });


               
                var authTicket = new AuthenticationTicket(identity, authProperties);

                context.Validated(authTicket);
                
            }
            else
            {
                context.SetError("invalid_access", "Invalid Username or passowrd");
               
                context.Rejected();
            }


            
             return base.GrantResourceOwnerCredentials(context);

            ;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return  Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach(KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            context.Identity.Claims.ToList<Claim>().ForEach(claim =>
            {
                context.AdditionalResponseParameters.Add(claim.Type, claim.Value);
            });
            return Task.FromResult<object>(null);
        }
    }
}