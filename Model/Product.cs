using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApi.Model
{
    public class Product
    {
        public long ProductId { get; set; }
        public string ProductReferenceNumber { get; set; }
        public string ProductName { get; set; }
    }
}