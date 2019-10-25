using Baasic.Client.Core;
using DReporting.Model;
using DReporting.Repository;
using DReporting.Service;
using DReporting.Service.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DReporting.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly string id = "urss5MSM5qcV6whV0EyvW0";
        private readonly string schemaName = "ReportModel";

        private IDynamicResourceService dynamicResourceService;
        private ReportModel reportModel;

        private readonly DIModuleService dIModuleService;
        private readonly DIModuleRepository dIModuleRepository;

        #region Constructors

        public ListController(IDynamicResourceService _dynamicResourceService, ReportModel _reportModel)
        {
            dynamicResourceService = _dynamicResourceService;
            reportModel = _reportModel;
        }

        #endregion

        #region Get/Post Index
        //GET
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            await dynamicResourceService.GetData(schemaName,
                                                                 id,
                                                                 ClientBase.DefaultEmbed,
                                                                 ClientBase.DefaultFields);
            return Ok();
                       
            /*
            var user = from i in queryValue where i.Id == id select i;
            
            if (user == null)
            {
                var responseBad = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return BadRequest(responseBad);
            }
            else
            {
                HttpRequestMessage request = new HttpRequestMessage();
                var responseOk = request.CreateResponse(HttpStatusCode.OK);
                return Ok(responseOk);
            }*/
        }
        [Route("list/all")]
        public ActionResult Post()
        {
            DateTime today = DateTime.Today;

            List<ReportModel> listModel = new List<ReportModel>();
            listModel.Add(new ReportModel
            {
                Date = today,
                Done = "Done upis",
                InProgress = "Trenutno je to",
                Scheduled = "Za sutra to",
                Problems = "Problemi, problemi",
                Description = "Neki poduzi opis"
            });

            listModel.Add(new ReportModel
            {
                Date = today,
                Done = "Done drugi upis",
                InProgress = "Trenutno je to drugi upis",
                Scheduled = "Za sutra to drugi upis",
                Problems = "Problemi, problemi drugi upis",
                Description = "Neki poduzi opis za drugi upis"
            });
            return Ok(listModel);
        }
        #endregion

        #region Get/Post Create
        //POST CREATE
        [HttpPost]
        public async Task<ActionResult> Create(ReportModel resource)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dynamicResourceService.InsertData(schemaName, resource);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to create changes.");
            }
            return Accepted(resource);
        }

        //GET CREATE
        public ActionResult Create()
        {
            return Accepted();
        }
        #endregion

        #region Get/Post Edit
        //POST EDIT
        [HttpPost]
        public async Task<ActionResult> Edit(ReportModel resource)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dynamicResourceService.UpdateData(schemaName, resource);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to edit changes");
            }
            return Accepted(resource);
        }

        //GET EDIT
        public ActionResult Edit(string id)
        {
            return Accepted(id);
        }
        #endregion

        #region Get/Post Delete
        
        //POST DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await dynamicResourceService.DeleteData(id);
            }
            catch
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        //GET DELETE
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                BadRequest();
            }
            return Accepted();
        }
        #endregion
    }
}
