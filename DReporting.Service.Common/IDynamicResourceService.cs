using DReporting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Service.Common
{
    public interface IDynamicResourceService<T> where T : ReportModel
    {
        Task<T> GetData(string schemaName, string id, string embed, string fields);
        Task FindData(string schemaName, string searchQuery, int page, int rpp, string sort, string embed, string fields);
        Task DeleteData(string id);
        Task<T> UpdateData(string schemaName, ReportModel resource);
        Task<T> InsertData(string schemaName, ReportModel resource);
        


    }
}
