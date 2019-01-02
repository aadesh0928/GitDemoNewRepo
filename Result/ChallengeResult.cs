using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SampleApi.Result
{
    public class ChallengeResult : IHttpActionResult
    {
        private readonly IHttpActionResult next;
        private readonly string realm;

        public ChallengeResult(IHttpActionResult next, string realm)
        {
            this.next = next;
            this.realm = realm;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(
                                    CancellationToken cancellationToken)
        {
            var res = await next.ExecuteAsync(cancellationToken);
            if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                res.Headers.WwwAuthenticate.Add(
                   new AuthenticationHeaderValue("Basic", this.realm));
            }

            return res;
        }
    }
}