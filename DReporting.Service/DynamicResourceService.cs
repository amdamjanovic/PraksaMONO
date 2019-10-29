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
    public class DynamicResourceService : IDynamicResourceService<ReportModel>
    {
        private readonly string id = "urss5MSM5qcV6whV0EyvW0";
        private readonly string resource = "ReportModel";

        protected IDynamicResourceRepository<ReportModel> DynamicResourceRepository { get; private set; }
        protected IValidationDictionary ValidationDictionary { get; private set; }
        
        #region Constructors
        public DynamicResourceService(IDynamicResourceRepository<ReportModel> dynamicResourceRepository, IValidationDictionary validationDictionary)
        {
            DynamicResourceRepository = dynamicResourceRepository;
            ValidationDictionary = validationDictionary;
        }
        #endregion

        #region Validation
        protected bool ValidateInput(ReportModel inputToValidate)
        {
            if (inputToValidate.Date == null)
            {
                ValidationDictionary.AddError("Date", "Date is required");
            }
            if (inputToValidate.Done.Trim().Length == 0)
            {
                ValidationDictionary.AddError("Done", "Input is required");
            }
            if (inputToValidate.InProgress.Trim().Length == 0)
            {
                ValidationDictionary.AddError("InProgress", "Input is required");
            }
            if (inputToValidate.Scheduled.Trim().Length == 0)
            {
                ValidationDictionary.AddError("Scheduled", "Input is required");
            }
            if (inputToValidate.Problems.Trim().Length == 0)
            {
                ValidationDictionary.AddError("Problems", "Input is required");
            }
            if (inputToValidate.Description.Trim().Length == 0)
            {
                ValidationDictionary.AddError("Description", "Input is required");
            }
            return ValidationDictionary.IsValid;
        }
        #endregion
        
        #region Methods
        
        public Task<ReportModel> GetData(string schemaName, string id, string embed, string fields)
        {
            return DynamicResourceRepository.GetData(schemaName, id, embed, fields);
        }

        public Task FindData(string schemaName, string searchQuery, int page, int rpp, string sort, string embed, string fields)
        {
            return DynamicResourceRepository.FindData(schemaName, searchQuery, page, rpp, sort, embed, fields);
        }

        public Task DeleteData(string id)
        {
            return DynamicResourceRepository.DeleteData(id);
        }

        public Task<ReportModel> InsertData(string schemaName, ReportModel resource)
        {
            return DynamicResourceRepository.InsertData(schemaName, resource);
        }

        public Task<ReportModel> UpdateData(string schemaName, ReportModel resource)
        {
            return DynamicResourceRepository.UpdateData(schemaName, resource);
        }
    }
    #endregion
    
}
