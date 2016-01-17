using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CodingTest.WebAPI.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            var userName = this.RequestContext.Principal.Identity.Name;
            return $"Hello, {userName}.";
        }
    }
}