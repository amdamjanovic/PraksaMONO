using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Core;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model;
using DReporting.Repository;
using DReporting.Repository.Common;
using DReporting.Service.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DReporting.Service
{
    public class DynamicResourceService : IDynamicResourceService
    {
        private readonly string id = "urss5MSM5qcV6whV0EyvW0";
        private readonly string resource = "ReportModel";
        
        private IDynamicResourceRepository dynamicResourceRepository;
        private IValidationDictionary validationDictionary;
        private readonly ReportModel reportModel;
        private readonly DIModuleService dIModuleService;
        private readonly IDependencyResolver dependencyResolver;

        #region Constructors
        public DynamicResourceService(IDynamicResourceRepository _dynamicResourceRepository)
        {
            dynamicResourceRepository = _dynamicResourceRepository;
        }
        
        public DynamicResourceService(IValidationDictionary _validationDictionary, ReportModel _reportModel)
        {
            validationDictionary = _validationDictionary;
            reportModel = _reportModel;
        }
        #endregion

        #region Validation
        protected bool ValidateInput(ReportModel inputToValidate)
        {
            if (inputToValidate.Date == null)
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
        #endregion
        
        #region Methods
        
        public Task GetData(string schemaName, string id, string embed, string fields)
        {
            return dynamicResourceRepository.GetData(schemaName, id, embed, fields);
        }

        public Task FindData(string schemaName, string searchQuery, int page, int rpp, string sort, string embed, string fields)
        {
            return dynamicResourceRepository.FindData(schemaName, searchQuery, page, rpp, sort, embed, fields);
        }

        public Task DeleteData(string id)
        {
            return dynamicResourceRepository.DeleteData(id);
        }

        public Task InsertData(string schemaName, ReportModel resource)
        {
            return dynamicResourceRepository.InsertData(schemaName, resource);
        }

        public Task UpdateData(string schemaName, ReportModel resource)
        {
            return dynamicResourceRepository.UpdateData(schemaName, resource);
        }
    }
    #endregion
    
}
