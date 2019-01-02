using SampleApi.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace SampleApi.Filter
{
    public class BasicAuthenticationFilter : Attribute, IAuthenticationFilter
    {

        private readonly string realm;
        public bool AllowMultiple {
            get
            {
                return false;
            }

        }

        public BasicAuthenticationFilter()
        {
            this.realm = $"realm={this.realm}";
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;

            if(request.Headers.Authorization!=null &&
                request.Headers.Authorization.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)){
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string credentials = encoding.GetString(Convert.FromBase64String(request.Headers.Authorization.Parameter));

                string[] parts = credentials.Split(':');
                string username = parts[0];
                string password = parts[1];

                if(username == password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("Role", "Admin")
                    };

                    var identity = new ClaimsIdentity(claims);

                    var principal = new ClaimsPrincipal(identity);

                    context.Principal = principal;

                }

            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], request);
            }


            return Task.FromResult(0);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            

            if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("basic", this.realm));
            }



            context.Result = new ResponseMessageResult(result);


            //context.Result = new ChallengeResult(context.Result, realm);

            //return Task.FromResult(0);
        }
    }
}