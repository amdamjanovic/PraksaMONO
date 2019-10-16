using DReporting.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DReporting.Repository.Common
{
    public interface IModelRepository
    {
        bool CreateModel(ReportModel modelToCreate);
        IEnumerable<ReportModel> ListModel();
    }
}
