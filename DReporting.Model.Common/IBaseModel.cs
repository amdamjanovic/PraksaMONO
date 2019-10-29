using Baasic.Client.Common;
using Baasic.Client.Model;
using Baasic.Client.Modules.DynamicResource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Model.Common
{
    public interface IBaseModel : IModel
    {
        Task<bool> DeleteAsync(SGuid id);
        Task<CollectionModelBase<IModel>> FindAsync(string searchQuery = "", int page = 1, int rpp = 10, string sort = "", string embed = "", string fields = "");
        Task<CollectionModelBase<IModel>> FindAsync(string schemaName, string searchQuery = "", int page = 1, int rpp = 10, string sort = "", string embed = "", string fields = "");
        Task<IModel> GetAsync(SGuid id, string embed = "", string fields = "");
        Task<IModel> GetAsync(string schemaName, SGuid id, string embed = "", string fields = "");
        Task<IModel> InsertAsync(IModel resource);
        Task<IModel> InsertAsync(string schemaName, IModel resource);
        Task<bool> PatchAsync<TIn>(SGuid id, TIn resource);
        Task<bool> PatchAsync<TIn>(string schemaName, SGuid id, TIn resource);
        Task<IModel> UpdateAsync(IModel resource);
        Task<IModel> UpdateAsync(string schemaName, IModel resource);
        
    }
}
