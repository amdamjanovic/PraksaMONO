using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Model;
using DReporting.Model;
using DReporting.Model.Common;
using DReporting.Repository;
using DReporting.Repository.Common;

namespace Baasic.Client.Modules.DynamicResource
{
    public class DynamicResourceRepository : IDynamicResourceRepository  {

        public string id = "urss5MSM5qcV6whV0EyvW0";
        public string schemaName = "ReportModel";
        
        private readonly ReportModel reportModel;
        private readonly IDynamicResourceClient<ReportModel> dynamicResourceClient;
        private readonly DIModuleRepository dIModuleRepository;

        #region Constructors
        public DynamicResourceRepository(IDynamicResourceClient<ReportModel> _dynamicResourceClient)
        {
            dynamicResourceClient = _dynamicResourceClient;
        }
        
        public DynamicResourceRepository(ReportModel _reportModel)
        {
            reportModel = _reportModel;
        }
        #endregion

        #region Methods
        //GET DATA
        public Task GetData(string schemaName, string id, string embed, string fields)
        {
            return dynamicResourceClient.GetAsync(schemaName, id, embed, fields);
        }

        //FIND DATA
        public Task FindData(string schemaName, string searchQuery, int page, int rpp, string sort, string embed, string fields)
        {
            return dynamicResourceClient.FindAsync(schemaName, searchQuery, page, rpp, sort, embed, fields);
        }

        //DELETE DATA    
        public Task DeleteData(string id)
        {
            return dynamicResourceClient.DeleteAsync(id);
        }

        //UPDATE DATA
        public Task UpdateData(string schemaName, ReportModel resource)
        {
            return dynamicResourceClient.UpdateAsync(schemaName, resource);
        }

        //INSERT DATA
        public Task InsertData(string schemaName, ReportModel resource)
        {
            return dynamicResourceClient.InsertAsync(schemaName, resource);
        }
        
        #endregion
    }
}
