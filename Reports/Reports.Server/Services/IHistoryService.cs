using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IHistoryService
    {
        HistoryUnit Create(DateTime modificationTime, Guid changer, Guid problemId);
        List<HistoryUnit> FindByEmployee(Guid employee);
        List<HistoryUnit> FindByTime(DateTime time);
    }
}