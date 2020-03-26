using System;
using System.Collections.Generic;
using InternalComplexTask.Data.Contracts.Entities.Enums;

namespace InternalComplexTask.Data.Contracts.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}