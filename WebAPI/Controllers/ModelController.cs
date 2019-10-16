using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model;
using DReporting.Service;
using DReporting.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using static Baasic.Client.Modules.DynamicResource.DynamicResourceRepository;

/* 
 The controller use service layer. 
 The service layer talks to the repository layer. 
 Each layer has a separate responsibility. 
*/

namespace DReporting.WebAPI.Controllers
{
    public class ModelController : Controller
    {
        private IModelService service;
        private readonly DynamicResourceService toService;

        public ModelController()
        {
            toService = new DynamicResourceService(new ModelStateWrapper(ModelState), new DynamicResourceRepository());
            service = (IModelService)toService;
        }

        public ModelController(IModelService _service)
        {
            service = _service;
        }

        public ActionResult Index()
        {
            return View(service.ListModel());
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        //POST
        public ActionResult Create(ReportModel modelToCreate)
        {
            if (!service.CreateModel(modelToCreate))
            {
                return View();
            }
            
            return RedirectToAction("Index");
        }


    }
}