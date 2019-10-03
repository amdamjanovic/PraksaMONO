using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Baasic.Client.Configuration;
using WebApi;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Inject the Configuration class via constructor
        private Configuration _configuration { get; set; }

        public ValuesController(Configuration configuration)
        {
            _configuration = configuration;
        }

        // GET api/values
        [HttpGet]
        public ActionResult <IEnumerable<string>> Get()
        {
           return new string[] {"ConnectionString: ", _configuration.ConnectionString};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
