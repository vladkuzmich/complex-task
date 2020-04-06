using InternalComplexTask.Data.Contracts.Entities.Enums;

namespace InternalComplexTask.Business.Contracts.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }
        public int DepartmentId { get; set; }
    }
}
