using System;

namespace Reports.DAL.Entities
{
    public class HistoryUnit
    {
        private HistoryUnit()
        {
        }

        public HistoryUnit(Guid id, Guid problemId, DateTime modificationTime, Guid changer)
        {
            ModificationTime = modificationTime;
            Changer = changer;
            Id = id;
            ProblemId = problemId;
        }
        public Guid Id { get; private init; }
        
        public DateTime ModificationTime { get; private init; }
        public Guid Changer { get; private init; }
        public Guid ProblemId { get; private init; }
    }
}