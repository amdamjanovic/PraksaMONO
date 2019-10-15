using DReporting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Service.Common
{
    public interface IDynamicResourceService
    {
        Task GetDataAsync(string schemaName, string id, string embed, string fields);
        Task DeleteDataAsync(string id);
        Task UpdateDataAsync(string schemaName, ReportModel resource);
        Task InsertDataAsync(string schemaName, ReportModel resource);

        
    }
}
