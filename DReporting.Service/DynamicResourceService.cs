using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model;
using DReporting.Model.Common;
using DReporting.Repository;
using DReporting.Repository.Common;
using DReporting.Service.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Baasic.Client.Modules.DynamicResource.DynamicResourceRepository;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace DReporting.Service
{
    public class DynamicResourceService : IDynamicResourceService
    {
        private IModelRepository modelRepostory;
        private DynamicResourceRepository dynamicResourceRepository;
        private IValidationDictionary validationDictionary;

        public DynamicResourceService(IValidationDictionary _validationDictionary, IModelRepository _modelRepostory, DynamicResourceRepository _dynamicResourceRepository)
        {
            modelRepostory = _modelRepostory;
            validationDictionary = _validationDictionary;
            dynamicResourceRepository = _dynamicResourceRepository;
        }
        
        public DynamicResourceService(IValidationDictionary _validationDictionary, DynamicResourceRepository _dynamicResourceRepository)
        {
            validationDictionary = _validationDictionary;
            dynamicResourceRepository = _dynamicResourceRepository;
        }
        
        protected bool ValidateInput(ReportModel inputToValidate)
        {
            if (inputToValidate.Date.Trim().Length == 0)
            {
                validationDictionary.AddError("Date", "Date is required");
            }
            if (inputToValidate.Done.Trim().Length == 0)
            {
                validationDictionary.AddError("Done", "Input is required");
            }
            if (inputToValidate.InProgress.Trim().Length == 0)
            {
                validationDictionary.AddError("InProgress", "Input is required");
            }
            if (inputToValidate.Scheduled.Trim().Length == 0)
            {
                validationDictionary.AddError("Scheduled", "Input is required");
            }
            if (inputToValidate.Problems.Trim().Length == 0)
            {
                validationDictionary.AddError("Problems", "Input is required");
            }
            if (inputToValidate.Description.Trim().Length == 0)
            {
                validationDictionary.AddError("Description", "Input is required");
            }
            return validationDictionary.IsValid;
        }

        public IEnumerable<ReportModel> ListModel()
        {
            return modelRepostory.ListModel();
        }

        public bool CreateModel(ReportModel modelToCreate)
        {
            if (!ValidateInput(modelToCreate))
            {
                return false;
            }
            try
            {
                modelRepostory.CreateModel(modelToCreate);
            }
            catch
            {
                return false;
            }return true;
        }

        public async Task DeleteDataAsync(string id)
        {
            await dynamicResourceRepository.DeleteDataAsync(id);
        }

        public async Task GetDataAsync(string schemaName, string id, string embed, string fields)
        {
            await dynamicResourceRepository.GetDataAsync(schemaName, id, ClientBase.DefaultEmbed, ClientBase.DefaultFields);
        }

        public async Task InsertDataAsync(string schemaName, ReportModel resource)
        {
            await dynamicResourceRepository.InsertDataAsync(schemaName, resource);
        }

        public async Task UpdateDataAsync(string schemaName, ReportModel resource)
        {
            await dynamicResourceRepository.UpdateDataAsync(schemaName, resource);
        }
    }

    public interface IModelService
    {
        bool CreateModel(ReportModel modelToCreate);
        IEnumerable<ReportModel> ListModel();
    }
}
