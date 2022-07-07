using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.DAL.Enums;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Problem Create(Guid id, DateTime creationDate, string comment, ProblemStatus problemStatus, Guid employee);
        Problem FindById(Guid id);
        List<Problem> FindByCreationDate(DateTime creationDateTime);
        List<Problem> FindByEmployee(Guid employee);
        Problem ChangeState(Problem problem, ProblemStatus problemStatus);
        Problem AddComment(Problem problem, string comment, Guid changer);
        List<Problem> FindByBoss(Guid bossId);
        List<Problem> GetAll();
        Problem Reassign(Guid problemId, Guid newEmployee);
    }
}