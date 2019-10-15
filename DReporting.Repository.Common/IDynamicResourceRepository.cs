using DReporting.Model;
using System;
using System.Threading.Tasks;

namespace DReporting.Repository.Common
{
    public interface IDynamicResourceRepository
    {
        Task GetDataAsync(string schemaName, string id, string embed, string fields);
        Task DeleteDataAsync(string id);
        Task UpdateDataAsync(string schemaName, ReportModel resource);
        Task InsertDataAsync(string schemaName, ReportModel resource);

        Task GetDataById(string id);
        
    }
}
