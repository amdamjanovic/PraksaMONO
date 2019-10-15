using Baasic.Client.Core;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model;
using DReporting.Model.Common;
using DReporting.Repository.Common;
using DReporting.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Baasic.Client.Modules.DynamicResource.DynamicResourceRepository;

namespace DReporting.WebAPI.Controllers
{
    public class CRUDController : Controller
    {
        private string id = "urss5MSM5qcV6whV0EyvW0";
        private string schemaName = "DReporting";
        
        private readonly IDynamicResourceRepository dynamicResourceRepository;
        MyDbContext context = new MyDbContext();
        
        public CRUDController()
        {
            dynamicResourceRepository = new DynamicResourceRepository(new ReportModel());
        }

        public CRUDController(IDynamicResourceRepository _dynamicResourceRepository)
        {
            dynamicResourceRepository = _dynamicResourceRepository;
        }

        //GET
        #region GetIndex
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date"; //The condition ? truevalue : falsevalue

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var queryValue = dynamicResourceRepository.GetDataAsync(schemaName,
                                                                    id,
                                                                    ClientBase.DefaultEmbed,
                                                                    ClientBase.DefaultFields) as IQueryable<ReportModel>;

            var user = from i in queryValue select i;

            switch (sortOrder)
            {
                case "Date":
                    user = user.OrderBy(i => i.Date);
                    break;
                default:
                    user = user.OrderBy(i => i.Date);
                    break;
            }
            return View(user);
        }
        #endregion
        public ActionResult Index()
        {
            return View(context.ReportModel.ToList());
        }

        //POST CREATE
        [HttpPost]
        public async Task<ActionResult> Create(ReportModel resource )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dynamicResourceRepository.InsertDataAsync(schemaName, resource);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to create changes.");
            }
            return View(resource);
        }

        //GET CREATE
        public ActionResult Create()
        {
            return View();
        }

        //POST EDIT
        [HttpPost]
        public async Task<ActionResult> Edit(ReportModel resource)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dynamicResourceRepository.UpdateDataAsync(schemaName, resource);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to edit changes");
            }
            return View(resource);
        }

        //GET EDIT
        public ActionResult Edit()
        {
            return View();
        }

        //GET DELETE
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed";
            }
            return View();
        }

        //POST DELETE
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await dynamicResourceRepository.DeleteDataAsync(id);
            }
            catch
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }
            return RedirectToAction("Index");
            
        }
    }
}
