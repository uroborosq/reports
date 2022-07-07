using System;
using System.Collections.Generic;
using System.Globalization;
using Reports.DAL.Entities;

namespace Reports.Clients
{
    public class Cli
    {
        private readonly Service _service;
        public Cli(Service service)
        {
            _service = service;
        }

        public void Exec()
        {
            while (true)
            {
                Console.WriteLine("Hello, choose one option");
                Console.WriteLine(
                    "Employees: \n 1. Create new \n 2. Create new teamlead \n 3. Find \n 4. Delete \n 5. Get All \n 6. Update existed");
                Console.WriteLine(
                    "Reports: \n 7. Create new \n 8. Get reports of this week \n 9. Get closed daily reports \n 10. Get employees with unclosed reports \n 11. Close report \n 12. Add resolved task to report \n 13. Get all reports \n 14. Find by id");
                Console.WriteLine(
                    "Tasks: \n 15. Create new task \n 16. Find task by id \n 17. Find by creation time \n 18. Find by modification time \n 19. Find by employee \n 20. Get all \n 21. Find tasks of subordinate");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateNewEmployee();
                        break;
                    case "2":
                        CreateTeamlead();
                        break;
                    case "3":
                        FindEmployeeById();
                        break;
                    case "4":
                        DeleteEmployee();
                        break;
                    case "5":
                        GetAllEmployees();
                        break;
                    case "6":
                        UpdateEmployee();
                        break;
                    case "7":
                        CreateNewReport();
                        break;
                    case "8":
                        WeekReport();
                        break;
                    case "9":
                        DailyClosedReports();
                        break;
                    case "10":
                        DailyUnclosedReports();
                        break;
                    case "11":
                        CloseReport();
                        break;
                    case "12":
                        AddTaskToReport();
                        break;
                    case "13":
                        GetAllReports();
                        break;
                    case "14":
                        FindReportById();
                        break;
                    case "15":
                        CreateNewTask();
                        break;
                    case "16":
                        FindTaskById();
                        break;
                    case "17":
                        FindByCreationTime();
                        break;
                    case "18":
                        FindByModificationTime();
                        break;
                    case "19":
                        FindByEmployee();
                        break;
                    case "20":
                        GetAll();
                        break;
                    case "21":
                        FindTaskOfSubordinates();
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
        }

        private void CreateNewEmployee()
        {
            Console.WriteLine("Type name of new employee:");
            string name = Console.ReadLine();
            Console.WriteLine("Type guid of his boss");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            
            try
            {
                var id = new Guid(strId);
                _service.CreateEmployee(name, id);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CreateTeamlead()
        {
            Console.WriteLine("Type name of new teamlead:");
            string name = Console.ReadLine();
            
            try
            {
                _service.CreateTeamlead(name);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FindEmployeeById()
        {
            Console.WriteLine("Type guid:");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
           
            try
            {
                var id = new Guid(strId);
                var employee = _service.FindById(id);
                Console.WriteLine($"Success\n Name: {employee.Name} \n Id: {employee.Id} \n BossId: {employee.BossId}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DeleteEmployee()
        {
            Console.WriteLine("Type guid");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            try
            {
                _service.Delete(id);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetAllEmployees()
        {
            try
            {
                var list = _service.GetAllEmployees();
                foreach (Employee employee in list)
                {
                    Console.WriteLine($"Success\n Name: {employee.Name} \n Id: {employee.Id} \n BossId: {employee.Id}");
                }
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void UpdateEmployee()
        {
            
            Console.WriteLine("Type guid");
            string strId = Console.ReadLine();
            Console.WriteLine("Type guid of new boss");
            string bossStrId = Console.ReadLine();

            if (strId == null || bossStrId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            var bossId = new Guid(bossStrId);
            try
            {
                _service.Update(id, bossId);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CreateNewReport()
        {
            Console.WriteLine("Type deadline:");
            string timeStr = Console.ReadLine();
            Console.WriteLine("Type guid of his boss");
            string strId = Console.ReadLine();

            if (strId == null || timeStr == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }

            try
            {
                var id = new Guid(strId);
                var time = DateTime.Parse(timeStr);
                _service.CreateReport(time, id);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void WeekReport()
        {
            try
            {
                var list = _service.GetOnThisWeek();
                foreach (Report report in list)
                {
                    Console.WriteLine($"Id: {report.Id} \n Deadline {report.Deadline.ToString(CultureInfo.InvariantCulture)} \n Owner: {report.Owner} \n Is closed: {report.IsClosed}");
                    Console.WriteLine("Tasks");
                    foreach (Guid reportResolvedTask in report.ResolvedTasks)
                    {
                        Console.WriteLine($"Guid: {reportResolvedTask}");
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DailyClosedReports()
        {
            try
            {
                var list = _service.GetDailyClosed();
                foreach (Report report in list)
                {
                    Console.WriteLine($"Id: {report.Id} \n Deadline {report.Deadline.ToString(CultureInfo.InvariantCulture)} \n Owner: {report.Owner} \n Is closed: {report.IsClosed}");
                    Console.WriteLine("Tasks");
                    foreach (Guid reportResolvedTask in report.ResolvedTasks)
                    {
                        Console.WriteLine($"Guid: {reportResolvedTask}");
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DailyUnclosedReports()
        {
            try
            {
                var list = _service.GetDailyUnclosed();
                foreach (Employee employee in list)
                {
                    Console.WriteLine($"Success\n Name: {employee.Name} \n Id: {employee.Id} \n BossId: {employee.Id}");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CloseReport()
        {
            Console.WriteLine("Type guid");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            try
            {
                _service.Close(id);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void AddTaskToReport()
        {
            Console.WriteLine("Type guid");
            string strId = Console.ReadLine();
            Console.WriteLine("Type guid of task");
            string taskStrId = Console.ReadLine();

            if (strId == null || taskStrId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            var taskId = new Guid(taskStrId);
            try
            {
                _service.AddTask(id, taskId);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetAllReports()
        {
            try
            {
                var list = _service.GetAllReports();
                foreach (Report report in list)
                {
                    Console.WriteLine($"Id: {report.Id} \n Deadline {report.Deadline.ToString(CultureInfo.InvariantCulture)} \n Owner: {report.Owner} \n Is closed: {report.IsClosed}");
                    Console.WriteLine("Tasks");
                    foreach (Guid reportResolvedTask in report.ResolvedTasks)
                    {
                        Console.WriteLine($"Guid: {reportResolvedTask}");
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FindReportById()
        {
            Console.WriteLine("Type guid:");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            try
            {
                Report report = _service.FindReportById(id);
                Console.WriteLine($"Id: {report.Id} \n Deadline {report.Deadline.ToString(CultureInfo.InvariantCulture)} \n Owner: {report.Owner} \n Is closed: {report.IsClosed}");
                Console.WriteLine("Tasks");
                foreach (Guid reportResolvedTask in report.ResolvedTasks)
                {
                    Console.WriteLine($"Guid: {reportResolvedTask}");
                }            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CreateNewTask()
        {
            Console.WriteLine("Type guid");
            string idStr = Console.ReadLine();
            Console.WriteLine("Type comment");
            string comment = Console.ReadLine();

            if (comment == null || idStr == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }

            try
            {
                var id = new Guid(idStr);
                _service.CreateTask(comment, id);
                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FindTaskById()
        {
            Console.WriteLine("Type guid:");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            try
            {
                var problem = _service.FindProblemById(id);
                Console.WriteLine($"Success\n Comment: {problem.Comment} \n Id: {problem.Id} \n CreationTime: {problem.CreationTime} \n Status: {problem.ProblemStatus}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FindByCreationTime()
        {
            Console.WriteLine("Type time:");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = DateTime.Parse(strId);
            try
            {
                List<Problem> list = _service.FindProblemByCreationTime(id);
                foreach (Problem problem in list)
                {
                    Console.WriteLine($"Success\n Comment: {problem.Comment} \n Id: {problem.Id} \n CreationTime: {problem.CreationTime} \n Status: {problem.ProblemStatus}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FindByModificationTime()
        {
            Console.WriteLine("Type time:");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = DateTime.Parse(strId);
            try
            {
                List<Problem> list = _service.FindProblemByModificationTime(id);
                foreach (Problem problem in list)
                {
                    Console.WriteLine($"Success\n Comment: {problem.Comment} \n Id: {problem.Id} \n CreationTime: {problem.CreationTime} \n Status: {problem.ProblemStatus}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FindByEmployee()
        {
            Console.WriteLine("Type guid:");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            try
            {
                var list = _service.FindByEmployee(id);
                foreach (Problem problem in list)
                {
                    Console.WriteLine($"Success\n Comment: {problem.Comment} \n Id: {problem.Id} \n CreationTime: {problem.CreationTime} \n Status: {problem.ProblemStatus}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetAll()
        {
            try
            {
                var list = _service.GetAllProblems();
                foreach (Problem problem in list)
                {
                    Console.WriteLine($"Success\n Comment: {problem.Comment} \n Id: {problem.Id} \n CreationTime: {problem.CreationTime} \n Status: {problem.ProblemStatus}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FindTaskOfSubordinates()
        {
            Console.WriteLine("Type guid:");
            string strId = Console.ReadLine();

            if (strId == null)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            var id = new Guid(strId);
            try
            {
                var list = _service.FindProblemsOfSubordinates(id);
                foreach (Problem problem in list)
                {
                    Console.WriteLine($"Success\n Comment: {problem.Comment} \n Id: {problem.Id} \n CreationTime: {problem.CreationTime} \n Status: {problem.ProblemStatus}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}