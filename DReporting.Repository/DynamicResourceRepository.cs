using System.Collections.Generic;
using System.Threading.Tasks;
using Baasic.Client.Core;
using Baasic.Client.Model;
using DReporting.Model;
using DReporting.Model.Common;
using DReporting.Repository.Common;

namespace Baasic.Client.Modules.DynamicResource
{
    public class DynamicResourceRepository : IDynamicResourceRepository  {

        public string id = "urss5MSM5qcV6whV0EyvW0";
        public string url = "https://api.baasic.com/v1/dreporting/resources/ReportModel/";
        public string schemaName = "DReporting";

        private readonly ReportModel reportModel;
        private readonly IBaseModel dynamicResourceClient;
        private readonly MyDbContext myDbContext;
        
        public DynamicResourceRepository(IBaseModel _dynamicResourceClient, MyDbContext _myDbContext)
        {
            dynamicResourceClient = _dynamicResourceClient;
            myDbContext = _myDbContext;
        }

        public DynamicResourceRepository()
        {
        }

        public DynamicResourceRepository(ReportModel reportModel)
        {
            this.reportModel = reportModel;
        }

        //GET DATA
        public async Task GetDataAsync(string schemaName, 
                                       string id, 
                                       string embed, 
                                       string fields)
        {
            await dynamicResourceClient.GetAsync(schemaName, id, ClientBase.DefaultEmbed, ClientBase.DefaultFields);
        }

        //DELETE DATA    
        public async Task DeleteDataAsync(string id)
        {
            await dynamicResourceClient.DeleteAsync(id);
        }

        //UPDATE DATA
        public async Task UpdateDataAsync(string schemaName, ReportModel resource)
        {
            await dynamicResourceClient.UpdateAsync(schemaName, resource);
        }

        //INSERT DATA
        public async Task InsertDataAsync(string schemaName, ReportModel resource)
        {
            await dynamicResourceClient.InsertAsync(schemaName, resource);
        }

        public Task GetDataById(string id)
        {
            throw new System.NotImplementedException();
        }

        public interface IModelRepository
        {
            bool CreateModel(ReportModel modelToCreate);
            IEnumerable<ReportModel> ListModel();
        }
    }
}
