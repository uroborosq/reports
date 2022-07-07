using System;
using System.Collections.Generic;
using System.Linq;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public Guid BossId { get; private init; }
        private Employee()
        {
        }

        public Employee(Guid id, string name, Guid bossId)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException( "Name is invalid");
            }

            Id = id;
            Name = name;
            BossId = bossId;
        }
    }
}