using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

using Reports.DAL.Entities;

namespace Reports.Server.Database
{
    public class ReportsRepo : IReportsRepo
    {
        private readonly string _reportsPath;
        private readonly string _problemsPath;
        private readonly string _historyPath;
        private readonly string _employeesPath;

        public ReportsRepo()
        {
            _reportsPath = "reports.json";
            _problemsPath = "problems.json";
            _historyPath = "history.json";
            _employeesPath = "employees.json";
            
            if (!File.Exists(_reportsPath)) File.Create(_reportsPath);
            if (!File.Exists(_problemsPath)) File.Create(_problemsPath);
            if (!File.Exists(_historyPath)) File.Create(_historyPath);
            if (!File.Exists(_employeesPath)) File.Create(_employeesPath);
            
            string str = File.ReadAllText(_employeesPath);
            Employees = str == "" ? new List<Employee>() : JsonConvert.DeserializeObject<Employee[]>(File.ReadAllText(_employeesPath, Encoding.UTF8)).ToList();
            str = File.ReadAllText(_problemsPath);
            Problems = str == "" ? new List<Problem>() : JsonConvert.DeserializeObject<Problem[]>(File.ReadAllText(_problemsPath, Encoding.UTF8)).ToList();
            str = File.ReadAllText(_historyPath);
            History= str == "" ? new List<HistoryUnit>() : JsonConvert.DeserializeObject<HistoryUnit[]>(File.ReadAllText(_historyPath, Encoding.UTF8)).ToList();
            str = File.ReadAllText(_reportsPath);
            Reports = str == "" ? new List<Report>() : JsonConvert.DeserializeObject<Report[]>(File.ReadAllText(_reportsPath, Encoding.UTF8)).ToList();
            

        }
        public ReportsRepo(string reportsPath, string problemPath, string employeesPath, string historyPath)
        {
            _reportsPath = reportsPath;
            _problemsPath = problemPath;
            _historyPath = historyPath;
            _employeesPath = employeesPath;
            if (!File.Exists(_reportsPath)) File.Create(_reportsPath);
            if (!File.Exists(_problemsPath)) File.Create(_problemsPath);
            if (!File.Exists(_historyPath)) File.Create(_historyPath);
            if (!File.Exists(_employeesPath)) File.Create(_employeesPath);
            
            string str = File.ReadAllText(_employeesPath);
            Employees = str == "" ? new List<Employee>() : JsonConvert.DeserializeObject<Employee[]>(File.ReadAllText(_employeesPath, Encoding.UTF8)).ToList();
            str = File.ReadAllText(_problemsPath);
            Problems = str == "" ? new List<Problem>() : JsonConvert.DeserializeObject<Problem[]>(File.ReadAllText(_problemsPath, Encoding.UTF8)).ToList();
            str = File.ReadAllText(_historyPath);
            History= str == "" ? new List<HistoryUnit>() : JsonConvert.DeserializeObject<HistoryUnit[]>(File.ReadAllText(_historyPath, Encoding.UTF8)).ToList();
            str = File.ReadAllText(_reportsPath);
            Reports = str == "" ? new List<Report>() : JsonConvert.DeserializeObject<Report[]>(File.ReadAllText(_reportsPath, Encoding.UTF8)).ToList();

        }

        public List<Employee> Employees { get; set; }
        public List<Problem> Problems { get; set; }
        public List<HistoryUnit> History { get; set; }
        public List<Report> Reports { get; set; }

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(Employees.ToArray());
            File.WriteAllText(_employeesPath, jsonString);
            jsonString = JsonSerializer.Serialize(History.ToArray());
            File.WriteAllText(_historyPath, jsonString);
            jsonString = JsonSerializer.Serialize(Reports.ToArray());
            File.WriteAllText(_reportsPath, jsonString);
            jsonString = JsonSerializer.Serialize(Problems.ToArray());
            File.WriteAllText(_problemsPath, jsonString);
        }
    }
}