using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApi.Model
{
    public class Order
    {
        public long OrderId { get; set; }
        public string OrderReferenceNumber { get; set; }
    }
}