using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Database
{
    public interface IReportsRepo
    {
        List<Employee> Employees { get; set; }
        List<Problem> Problems { get; set; }
        List<HistoryUnit> History { get; set; }
        List<Report> Reports { get; set; }
        void Save();
    }
}