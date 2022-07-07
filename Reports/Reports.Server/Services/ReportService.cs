using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportsRepo _context;

        public ReportService(IReportsRepo repo)
        {
            _context = repo;
        }


        public Report Create(Guid employee, DateTime deadline)
        {
            var report = new Report(Guid.NewGuid(), employee, deadline, false);
            _context.Reports.Add(report);
            _context.Save();
            return report;
        }

        public List<Problem> FindResolvedProblemsByThisWeek()
        {
            var list = new List<Problem>();
            foreach (DateTime d1 in from report in _context.Reports let cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar let d1 = DateTime.Now.Date.AddDays(-1 * (int)cal.GetDayOfWeek(DateTime.Now)) let d2 = report.Deadline.Date.AddDays(-1 * (int)cal.GetDayOfWeek(report.Deadline)) where d1 == d2 select d1)
            {
                list.AddRange(new List<Problem>());
            }

            return list;
        }

        public Report AddResolvedTask(Guid problem, Guid reportId)
        {
            Report report = FindById(reportId);
            if (!_context.Reports.Contains(report))
                throw new ArgumentException("There is no such report", nameof(reportId));

            Problem prob = _context.Problems.FirstOrDefault(pr => pr.Id == problem);
            report.AddResolvedProblem(prob);
            _context.Save();
            return report;
        }

        public Report Close(Guid reportId)
        {
            Report report = FindById(reportId);
            if (report == null)
                throw new ArgumentException("There is no such report", nameof(reportId));
            _context.Reports.Remove(report);
            report.Close();
            _context.Reports.Add(report);
            _context.Save();
            return report;
        }

        public List<Report> FindDailyClosedReports()
        {
            return _context.Reports.FindAll(report => report.Deadline.Date == DateTime.Today && report.IsClosed == true).ToList();
        }

        public List<Employee> FindDalyUnclosed()
        {
            return _context.Reports.FindAll(report => report.IsClosed == false && report.Deadline == DateTime.Today).Select(report => report.Owner).Select(id => _context.Employees.FirstOrDefault(employee => employee.Id == id)).ToList();
        }

        public Report FindById(Guid reportId)
        {
            return _context.Reports.FirstOrDefault(report => report.Id == reportId);
        }

        public List<Report> GetAll()
        {
            return _context.Reports.ToList();
        }
    }
}