using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.DAL.Enums;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IHistoryService _historyService;

        public TaskController(ITaskService taskService, IHistoryService historyService)
        {
            _taskService = taskService;
            _historyService = historyService;
        }
        
        [HttpPost]
        [Route("/tasks/create")]
        public Problem Create([FromQuery] string comment, [FromQuery] Guid employeeId)
        {
            return _taskService.Create(Guid.NewGuid(), DateTime.Now, comment, ProblemStatus.Open, employeeId);
        }

        [HttpGet]
        [Route("/tasks/findById")]
        public Problem FindById([FromQuery] Guid id)
        {
            return _taskService.FindById(id);
        }

        [HttpGet]
        [Route("/tasks/findByCreationTime")]
        public List<Problem> FindByCreationTime([FromQuery] DateTime dateTime)
        {
            return _taskService.FindByCreationDate(dateTime);
        }

        [HttpGet]
        [Route("/tasks/findByModificationTime")]
        public List<Problem> FindByModificationTime([FromQuery] DateTime dateTime)
        {
            List<HistoryUnit> units = _historyService.FindByTime(dateTime);
            return units.Select(unit => _taskService.FindById(unit.ProblemId)).ToList();
        }

        [HttpGet]
        [Route("/tasks/findByEmployee")]
        public List<Problem> FindByEmployee([FromQuery] Guid id)
        {
            return _taskService.FindByEmployee(id);
        }

        [HttpGet]
        [Route("/tasks/getAll")]
        public List<Problem> FindAll()
        {
            return _taskService.GetAll();
        }

        [HttpGet]
        [Route("/tasks/findTasksOfSubordinates")]
        public List<Problem> FindTasksOfSubordinates([FromQuery] Guid bossId)
        {
            return _taskService.FindByBoss(bossId);
        }

        [HttpPatch]
        [Route("/tasks/editComment")]
        public IActionResult EditComment([FromQuery] Guid problemId, [FromQuery] string newComment, [FromQuery] Guid changerId)
        {
            if (problemId == Guid.Empty || changerId == Guid.Empty)
                return BadRequest();
            Problem problem = FindById(problemId);
            if (problem == null)
                return NotFound();

            _taskService.AddComment(problem, newComment, changerId);
            _historyService.Create(DateTime.Now, changerId, problemId);
            return Ok();
        }

        [HttpPatch]
        [Route("/tasks/replaceEmployee")]
        public IActionResult ReplaceEmployee([FromQuery] Guid problemId, [FromQuery] Guid newEmployee)
        {
            if (problemId == Guid.Empty) return BadRequest();
            if (newEmployee == Guid.Empty) return BadRequest();
            
            return Ok(_taskService.Reassign(problemId, newEmployee));
        }

        [HttpPatch]
        [Route("/tasks/changeState")]
        public IActionResult ChangeState([FromQuery] Guid problemId, [FromQuery] ProblemStatus status)
        {
            if (problemId == Guid.Empty) return BadRequest();
            Problem problem = FindById(problemId);
            if (problem == null)
                return NotFound();
            
            return Ok(_taskService.ChangeState(problem, status));
        }
    }
}