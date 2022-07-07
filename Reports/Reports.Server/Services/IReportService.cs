using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Report Create(Guid employee, DateTime deadline);
        List<Problem> FindResolvedProblemsByThisWeek();
        Report AddResolvedTask(Guid problem, Guid reportId);
        Report Close(Guid reportId);
        List<Report> FindDailyClosedReports();
        List<Employee> FindDalyUnclosed();
        List<Report> GetAll();
        Report FindById(Guid reportId);
    }
}