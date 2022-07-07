using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Entities;
using Reports.DAL.Enums;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly IReportsRepo _context;

        public TaskService(IReportsRepo repo)
        {
            _context = repo;
        }


        public Problem Create(Guid id, DateTime creationDate, string comment, ProblemStatus problemStatus, Guid employee)
        {
            var problem = new Problem(id, creationDate, comment, problemStatus, employee);
            _context.Problems.Add(problem);
            _context.Save();
            return problem;
        }

        public Problem FindById(Guid id)
        {
            Problem problem = _context.Problems.FirstOrDefault(problem => problem.Id == id);
            return problem;
        }

        public List<Problem> FindByCreationDate(DateTime creationDateTime)
        {
            return _context.Problems.FindAll(problem => problem.CreationTime.Date == creationDateTime.Date).ToList();
        }

        public List<Problem> FindByEmployee(Guid employee)
        { 
            return _context.Problems.FindAll(problem => problem.Employee == employee).ToList();
        }

        public Problem ChangeState(Problem problem, ProblemStatus problemStatus)
        {
            if (!_context.Problems.Contains(problem))
                throw new ArgumentException("There is no such problem");
            problem.ProblemStatus = problemStatus;
            _context.Save();
            return problem;
        }

        public Problem AddComment(Problem problem, string comment, Guid changer)
        {
            if (!_context.Problems.Contains(problem))
                throw new ArgumentException("There is no such problem");
            problem.EditComment(comment, changer);
            
            _context.Save();
            return problem;
        }

        public List<Problem> FindByBoss(Guid bossId)
        {
            List<Employee> subordinates = _context.Employees.FindAll(employee => employee.BossId == bossId);
            var list = new List<Problem>();
            foreach (Employee employee in subordinates)
            {
                list.AddRange(FindByEmployee(employee.Id));
            }

            return list;
        }

        public List<Problem> GetAll()
        {
            return _context.Problems;
        }

        public Problem Reassign(Guid problemId, Guid newEmployee)
        {
            Problem problem = FindById(problemId);
            string comment = problem.Comment;
            DateTime time = problem.CreationTime;
            ProblemStatus status = problem.ProblemStatus;
            Delete(problemId);
            return Create(problemId, time, comment, status, newEmployee);
        }

        public void Delete(Guid problemId)
        {
            Problem problem = FindById(problemId);
            if (problem != null)
            {
                _context.Problems.Remove(problem);
            }
        }
    }
}