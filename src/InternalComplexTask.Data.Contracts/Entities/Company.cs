using System.Collections.Generic;

namespace InternalComplexTask.Data.Contracts.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
