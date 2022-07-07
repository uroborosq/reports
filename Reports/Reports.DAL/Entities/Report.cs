using System;
using System.Collections.Generic;
using Reports.DAL.Enums;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public Guid Id { get; private init; }
        public List<Guid> ResolvedTasks { get; private init; }
        public Guid Owner { get; private init; }
        public DateTime Deadline { get; private init; }
        public bool IsClosed { get; private set; }

        private Report()
        {
        }

        public Report(Guid id, Guid owner, DateTime deadline, bool isClosed)
        {
            if (deadline <= DateTime.Now) throw new ArgumentException("Deadline is out already", nameof(deadline));

            Owner = owner;
            Deadline = deadline;
            ResolvedTasks = new List<Guid>();
            IsClosed = isClosed;
            Id = id;
        }

        public void AddResolvedProblem(Problem problem)
        {
            Update();
            if (problem.ProblemStatus == ProblemStatus.Resolved && Deadline >= problem.CreationTime && IsClosed == false)
            {
                ResolvedTasks.Add(problem.Id);
            }
            else
            {
                throw new ArgumentException("Problem is not solved or deadline has been already out");
            }
        }
        
        public void Close()
        {
            IsClosed = true;
        }

        public void Update()
        {
            if (Deadline <= DateTime.Now)
                IsClosed = true;
        }
    }
}