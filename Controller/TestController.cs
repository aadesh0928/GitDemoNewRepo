using SampleApi.Filter;
using SampleApi.Model;
using SampleApi.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleApi.Controller
{
    [RoutePrefix("api/customers")]
    public class TestController : ApiController
    {
        private static List<Customer> customers = new List<Customer>
        {
            new Customer{ Id = 1, FirstName = "James", LastName ="Wilson", Address="United Kingdom"},
            new Customer{ Id = 2, FirstName = "Aadesh", LastName ="Yadav", Address="India"},
            new Customer{ Id = 3, FirstName = "ravi randev", LastName ="singh", Address="India"},
            new Customer{ Id = 4, FirstName = "Mike", LastName ="brown", Address="Us"},
            new Customer{ Id = 5, FirstName = "Laurie", LastName ="dvorak", Address="Us"},
            new Customer{ Id = 6, FirstName = "Alex", LastName ="Dowlding", Address="Us"}
        };

        [HttpGet]
        //[BasicAuthenticationFilter]
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(customers);
            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        //[VersionedRoute("{id}", 1)]
        public HttpResponseMessage Get(long id)
        {
            //try
            //{
            if (customers.Any(customer => customer.Id == id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, customers.FirstOrDefault(customer => customer.Id == id));
            }
            else
            {

                var message = $"Customer with Id : {id} not found";

                //var resposne = new HttpResponseMessage(HttpStatusCode.NotFound);

                //    resposne.ReasonPhrase = message;

                //    throw new HttpResponseException(resposne);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);


                // throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));


            }
            //}
            //catch (Exception)
            //{

            //    return InternalServerError();
            //}
        }

        [HttpGet]
        [VersionedRoute("{id}", 2)]
        public IHttpActionResult GetV2(long id)
        {
            try
            {
                if (customers.Any(customer => customer.Id == id))
                {
                    return Ok(customers.Single(customer => customer.Id == id));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                customer.Id = customers.Count + 1;
                customers.Add(customer);
                var x = customer.ToString();
                return Created(new Uri($"http://localhost:49806/API/customers/{(customers.Count).ToString()}"), customer);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }


        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody] Customer customer, [FromUri] long? id)
        {
            try
            {
                if (id == default(long?) || id == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Customer Id is not provided");
                }

                var custTobeUpdated = customers.Where(cust => cust.Id == id).FirstOrDefault();

                if (custTobeUpdated == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Customer with customer id : {id} not found");
                }


                custTobeUpdated.FirstName = customer.FirstName;
                custTobeUpdated.LastName = customer.LastName;
                custTobeUpdated.Address = customer.Address;

                return Request.CreateResponse(HttpStatusCode.OK, custTobeUpdated);
            }
            catch (Exception)
            {


                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }

        Func<string, string, string, Customer> createCustomer = (firstName, lastName, address) =>
       {
           var customer = new Customer { FirstName = firstName, LastName = lastName, Address = address };

           return customer;
       };


        Func<string, string, string, IEnumerable<Customer>> filterCustomers = (sortBy, firstName, lastname) =>
        {
            return
            customers
            .Where(cust => 
                        cust.FirstName.Equals(firstName) &&
                        cust.LastName.Equals(lastname)
                   )
            .OrderBy(cust => cust.FirstName);
        };
   
    }
}
 