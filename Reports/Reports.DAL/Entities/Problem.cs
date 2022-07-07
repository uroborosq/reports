using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Enums;

namespace Reports.DAL.Entities
{
    public class Problem
    {
        public Guid Id { get; private init; }
        public DateTime CreationTime { get; private init; }
        public string Comment { get; private set; }
        public ProblemStatus ProblemStatus { get; set; }
        public Guid Employee { get; private init; }

        private Problem()
        {
        }

        public Problem(Guid id, DateTime creationTime, string comment, ProblemStatus problemStatus, Guid employee)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }
            Id = id;
            Comment = comment;
            CreationTime = creationTime;
            ProblemStatus = problemStatus;
            Employee = employee;
        }

        public void EditComment(string comment, Guid employee)
        {
            Comment = comment ?? throw new NullReferenceException();
        }
    }
}