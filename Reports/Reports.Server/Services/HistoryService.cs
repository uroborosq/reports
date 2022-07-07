using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IReportsRepo _context;
        public HistoryService(IReportsRepo context)
        {
            _context = context;
        }
        
        public HistoryUnit Create(DateTime modificationTime, Guid changer, Guid problemId)
        {
            var historyUnit = new HistoryUnit(Guid.NewGuid(), problemId, modificationTime, changer);
            _context.History.Add(historyUnit);
            _context.Save();
            return historyUnit;
        }

        public List<HistoryUnit> FindByEmployee(Guid employee)
        {
            return _context.History.FindAll(unit => unit.Changer == employee).ToList();
        }

        public List<HistoryUnit> FindByTime(DateTime time)
        {
            return _context.History.FindAll(unit => unit.ModificationTime == time).ToList();
        }
    }
}