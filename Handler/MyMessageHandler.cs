using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SampleApi.Handler
{
    public class MyMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var response = base.SendAsync(request, cancellationToken);
            //response.Headers.Add("X-Aadesh-Email", "aadesh.yadav@nagarro.com");
            return base.SendAsync(request, cancellationToken).ContinueWith(
                (task) =>
                {
                    HttpResponseMessage response = task.Result;
                    response.Headers.Add("X-Custom-Header", "My Custom header");
                    return response;
                });
        }
    }
}