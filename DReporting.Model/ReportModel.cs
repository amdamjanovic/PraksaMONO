using Baasic.Client.Common;
using Baasic.Client.Model;
using DReporting.Model.Common;
using System;

namespace DReporting.Model
{
    public class ReportModel : IModel
    {
        public string Date { get; set; }
        public string Done { get; set; }
        public string InProgress { get; set; }
        public string Scheduled { get; set; }
        public string Problems { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public SGuid Id { get; set; }
    }
}
