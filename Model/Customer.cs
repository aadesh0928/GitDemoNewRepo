using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SampleApi.Model
{
    public class Customer

    {
        public Guid MyGuid { get; } = Guid.NewGuid();
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }


        public override string ToString() => $"FirstName : {FirstName}, LastName  : {LastName}, Address : {Address}";
    }
}