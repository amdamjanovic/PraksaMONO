using System.Threading.Tasks;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model;
using DReporting.Repository.Common;

namespace DReporting.Repository
{
    public class DynamicResourceRepository : IDynamicResourceRepository<ReportModel>  {

        private string id = "urss5MSM5qcV6whV0EyvW0";
        private string schemaName = "ReportModel";
        
        protected IDynamicResourceClient<ReportModel> DynamicResourceClient { get; private set; }
        protected IClientConfiguration ClientConfiguration { get; private set; }

        #region Constructors
        public DynamicResourceRepository(IDynamicResourceClient<ReportModel> dynamicResourceClient)
        {
            DynamicResourceClient = dynamicResourceClient;
        }
        #endregion

        #region Methods
        //GET DATA
        public Task<ReportModel> GetData(string schemaName, string id, string embed, string fields)
        {
            return DynamicResourceClient.GetAsync(schemaName, id, embed, fields);
        }

        //FIND DATA
        public Task FindData(string schemaName, string searchQuery, int page, int rpp, string sort, string embed, string fields)
        {
            return DynamicResourceClient.FindAsync(schemaName, searchQuery, page, rpp, sort, embed, fields);
        }

        //DELETE DATA    
        public Task DeleteData(string id)
        {
            return DynamicResourceClient.DeleteAsync(id);
        }

        //UPDATE DATA
        public Task<ReportModel> UpdateData(string schemaName, ReportModel resource)
        {
            return DynamicResourceClient.UpdateAsync(schemaName, resource);
        }

        //INSERT DATA
        public Task<ReportModel>InsertData(string schemaName, ReportModel resource)
        {
            return DynamicResourceClient.InsertAsync(schemaName, resource);
        }

        #endregion
    }
}
