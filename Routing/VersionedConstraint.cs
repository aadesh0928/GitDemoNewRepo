using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace SampleApi.Routing
{
    public class VersionedConstraint : IHttpRouteConstraint
    {
        public const string VersionHeaderName = "api-version";
        private const int DefaultVersion = 1;

        public VersionedConstraint(int allowedVersion)
        {
            this.AllowedVersion = allowedVersion;
        }
        public int AllowedVersion
        {
            private set;
            get;
        }
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if(routeDirection == HttpRouteDirection.UriResolution)
            {
                int version = GetVersionFromCustomHeader(request) ?? DefaultVersion;

                return ((version == AllowedVersion));
            }

            return true;
        }


        private int? GetVersionFromCustomHeader(HttpRequestMessage request)
        {
            string versionAsString;
            IEnumerable<string> headerValues;

            if(request.Headers.TryGetValues(VersionHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }
            else
            {
                return default(int?);
            }

            int version;
            if(versionAsString != null && int.TryParse(versionAsString, out version))
            {
                return version;
            }

            return default(int?);

        }
    }
}