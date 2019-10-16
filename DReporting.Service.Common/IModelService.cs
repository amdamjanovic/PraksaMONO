using DReporting.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DReporting.Service.Common
{
    public interface IModelService
    {
        bool CreateModel(ReportModel modelToCreate);
        IEnumerable<ReportModel> ListModel();
    }
}
