using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.DAL.Enums;

namespace Reports.Clients
{
    public class Service
    {
        private readonly string _url;
        public Service(string url)
        {
            _url = url;
        }

        public void CreateEmployee(string name, Guid bossId)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/create/?name={name}&bossId={bossId}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            response.GetResponseStream();
        }

        public void CreateTeamlead(string name)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/createTeamlead/?name={name}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            response.GetResponseStream();
        }

        public Employee FindById(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<Employee>(responseString);
        }

        public Employee FindByName(string name)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<Employee>(responseString);
        }

        public void Delete(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/delete?id={id}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

        public List<Employee> GetAllEmployees()
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/getall");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Employee>>(responseString);
        }

        public void Update(Guid id, Guid bossId)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/update/?id={id}&bossId={bossId}");
            request.Method = WebRequestMethods.Http.Put;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

        public void CreateReport(DateTime deadline, Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/create?deadline={deadline}&employeeId={id}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
        }

        public List<Report> GetOnThisWeek()
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/getOnThisWeek");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Report>>(responseString);
        }

        public List<Report> GetDailyClosed()
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/getDailyClosed");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Report>>(responseString);
        }

        public List<Employee> GetDailyUnclosed()
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/getDailyUnclosed");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Employee>>(responseString);
        }

        public List<Report> GetAllReports()
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/getAll");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Report>>(responseString);
        }

        public Report FindReportById(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/findById?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<Report>(responseString);
        }

        public void AddTask(Guid reportId, Guid problemId)
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/addTask?reportId={reportId}&problemId={problemId}");
            request.Method = WebRequestMethods.Http.Put;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

        public void Close(Guid reportId)
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/close?reportId={reportId}");
            request.Method = WebRequestMethods.Http.Put;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

        public void CreateTask(string comment, Guid employeeId)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/create?comment={comment}&employeeId={employeeId}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

        public Problem FindProblemById(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/findById?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<Problem>(responseString);
        }

        public List<Problem> FindProblemByModificationTime(DateTime time)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/findByModificationTime?dateTime={time}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Problem>>(responseString);
        }

        public List<Problem> FindProblemByCreationTime(DateTime time)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/findByCreationTime?dateTime={time}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Problem>>(responseString);
        }

        public List<Problem> FindByEmployee(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/findByEmployee?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Problem>>(responseString);
        }

        public List<Problem> GetAllProblems()
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/getAll");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Problem>>(responseString);
        }

        public List<Problem> FindProblemsOfSubordinates(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks//tasks/findTasksOfSubordinates?bossId={id}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Problem>>(responseString);
        }

        public void EditComment(Guid problemId, string newComment, Guid changerId)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/editComment?problemId={problemId}&newComment={newComment}&changerId={changerId}");
            request.Method = WebRequestMethods.Http.Put;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

        public void Reassign(Guid problemId, Guid newEmployeeId)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks//tasks/replaceEmployee?problemId={problemId}&newEmployee={newEmployeeId}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

        public void ChangeState(Guid problemId, ProblemStatus status)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks//tasks/changeState?problemId={problemId}&status={status}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        }

    }
}