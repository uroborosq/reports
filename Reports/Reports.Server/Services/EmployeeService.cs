using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.Server.Controllers;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IReportsRepo _context;

        public EmployeeService(IReportsRepo context)
        {
            _context = context;
        }

        public Employee Create(string name, Guid bossId)
        {
            var employee = new Employee(Guid.NewGuid(), name, bossId);
            _context.Employees.Add(employee);
            _context.Save();
            return employee;
        }

        public Employee FindByName(string name)
        {
            Employee employee = _context.Employees.FirstOrDefault(employee => employee.Name == name);
            return employee;
        }

        public Employee FindById(Guid id)
        {
            Employee employee = _context.Employees.FirstOrDefault(employee => employee.Id == id);
            return employee;
        }

        public void Delete(Guid id)
        {
            Employee employee = FindById(id);
            _context.Employees.Remove(employee);
            _context.Save();
        }

        public Employee Update(Employee entity)
        {
            Delete(entity.Id);
            _context.Employees.Add(entity);
            _context.Save();
            return entity;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}