using Baasic.Client.Common;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Model
{
    public class BaseModel : IBaseModel
    {
        private IBaseModel baseModel;

        public BaseModel(IBaseModel _baseModel)
        {
            baseModel = _baseModel;
        }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public SGuid Id { get; set; }

        public int Count(Func<IModel, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(SGuid id) => baseModel.DeleteAsync(id);

        public Task<CollectionModelBase<IModel>> FindAsync(string searchQuery = "", int page = 1, int rpp = 10, string sort = "", string embed = "", string fields = "") => baseModel.FindAsync(ClientBase.DefaultSearchQuery, ClientBase.DefaultPage, ClientBase.DefaultMaxNumberOfResults, ClientBase.DefaultSorting, ClientBase.DefaultEmbed, ClientBase.DefaultFields);

        public Task<CollectionModelBase<IModel>> FindAsync(string schemaName, string searchQuery = "", int page = 1, int rpp = 10, string sort = "", string embed = "", string fields = "") => baseModel.FindAsync(schemaName, ClientBase.DefaultSearchQuery, ClientBase.DefaultPage, ClientBase.DefaultMaxNumberOfResults, ClientBase.DefaultSorting, ClientBase.DefaultEmbed, ClientBase.DefaultFields);

        public Task<IModel> GetAsync(SGuid id, string embed = "", string fields = "") => baseModel.GetAsync(id, ClientBase.DefaultEmbed, ClientBase.DefaultFields);

        public Task<IModel> GetAsync(string schemaName, SGuid id, string embed = "", string fields = "") => baseModel.GetAsync(schemaName, id, ClientBase.DefaultEmbed, ClientBase.DefaultFields);

        public Task<IModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IModel> InsertAsync(IModel resource) => baseModel.InsertAsync(resource);

        public Task<IModel> InsertAsync(string schemaName, IModel resource) => baseModel.InsertAsync(schemaName, resource);

        public Task<bool> PatchAsync<TIn>(SGuid id, TIn resource) => baseModel.PatchAsync(id, resource);

        public Task<bool> PatchAsync<TIn>(string schemaName, SGuid id, TIn resource) => baseModel.PatchAsync(schemaName, id, resource);

        public Task<IModel> UpdateAsync(IModel resource) => baseModel.UpdateAsync(resource);

        public Task<IModel> UpdateAsync(string schemaName, IModel resource) => baseModel.UpdateAsync(schemaName, resource);
        
    }


}
