using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Database;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [Route("/reports/create")]
        public Report Create([FromQuery] DateTime deadline, [FromQuery] Guid employeeId)
        {
            return _reportService.Create(employeeId, deadline);
        }

        [HttpGet]
        [Route("/reports/getOnThisWeek")]
        public List<Problem> FindOnThisWeek()
        {
            return _reportService.FindResolvedProblemsByThisWeek();
        }

        [HttpGet]
        [Route("/reports/getDailyClosed")]
        public List<Report> FindDailyClosed()
        {
            return _reportService.FindDailyClosedReports();
        }

        [HttpGet]
        [Route("/reports/findDailyUnclosed")]
        public List<Employee> GetDailyUnclosed()
        {
            return _reportService.FindDalyUnclosed();
        }

        [HttpPatch]
        [Route("/reports/close")]
        public IActionResult Close([FromQuery] Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            Report report = _reportService.Close(id);
            if (report == null) return NotFound();

            return Ok(report);
        }

        [HttpPatch]
        [Route("/reports/addTask")]
        public IActionResult AddTask([FromQuery] Guid reportId, [FromQuery] Guid problemId)
        {
            Report result = _reportService.AddResolvedTask(problemId, reportId);
            return Ok(result);
        }

        [HttpGet]
        [Route("/reports/getAll")]
        public IActionResult GetAll()
        {
            return Ok(_reportService.GetAll());
        }

        [HttpGet]
        [Route("/reports/findById")]
        public IActionResult FindById([FromQuery] Guid reportId)
        {
            return Ok(_reportService.FindById(reportId));
        }
}
}