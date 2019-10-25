using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Baasic.Client.Configuration;
using DReporting.Model;
using DReporting.Service.Common;
using Baasic.Client.Core;
using DReporting.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DynamicResourceService dynamicResourceService;
        private readonly string schemaName = "ReportModel";
        private readonly string id = "urss5MSM5qcV6whV0EyvW0";

        private ReportModel configModel { get; set; }
        
        public ValuesController(DynamicResourceService _dynamicResourceService, ReportModel _configuration)
        {
            dynamicResourceService = _dynamicResourceService;
            configModel = _configuration;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var getData = dynamicResourceService.GetData(schemaName, id, ClientBase.DefaultEmbed, ClientBase.DefaultFields);
            return Ok(getData);
            
           //return new string[] {"Done:", configModel.Done};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] ReportModel resource)
        {
            Ok(dynamicResourceService.InsertData(schemaName, resource));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ReportModel resource)
        {
            Ok(dynamicResourceService.UpdateData(schemaName, resource));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Ok(dynamicResourceService.DeleteData(id));
        }
    }
}
