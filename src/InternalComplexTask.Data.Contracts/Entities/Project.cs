using System.Collections.Generic;

namespace InternalComplexTask.Data.Contracts.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
