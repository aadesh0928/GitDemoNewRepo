using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;

namespace SampleApi.Routing
{
    public class VersionedRoute : RouteFactoryAttribute
    {
        public int AllowedVersion { get; private set; }
        public VersionedRoute(string template, int allowedVersion) :  base(template)
        {
            this.AllowedVersion = allowedVersion;
        }
        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("version", new VersionedConstraint(AllowedVersion));

                return constraints;
            }
        }
    }
}