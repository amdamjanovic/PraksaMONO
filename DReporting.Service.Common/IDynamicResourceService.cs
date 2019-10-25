using DReporting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Service.Common
{
    public interface IDynamicResourceService
    {
        Task GetData(string schemaName, string id, string embed, string fields);
        Task DeleteData(string id);
        Task UpdateData(string schemaName, ReportModel resource);
        Task InsertData(string schemaName, ReportModel resource);
        Task FindData(string schemaName, string searchQuery, int page, int rpp, string sort, string embed, string fields);


    }
}
