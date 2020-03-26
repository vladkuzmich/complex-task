using System.Collections.Generic;
using System.Linq;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.API.Models.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto ToEmployeeDto(this EmployeeDocument employeeDocument)
        {
            return new EmployeeDto
                {
                    Id = employeeDocument.Id,
                    Name = employeeDocument.Name,
                    Surname = employeeDocument.Surname,
                    DepartmentId = employeeDocument.DepartmentId,
                    Gender = employeeDocument.Gender
                };
        }

        public static IEnumerable<EmployeeDto> ToEmployeeDtos(this IEnumerable<EmployeeDocument> employeeDocuments)
        {
            return employeeDocuments.Select(x => x.ToEmployeeDto());
        }

        public static EmployeeDocument ToEmployeeDocument(this EmployeeDto employeeDto)
        {
            return new EmployeeDocument
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Gender = employeeDto.Gender,
                DepartmentId = employeeDto.DepartmentId
            };
        }

        public static IEnumerable<EmployeeDocument> ToEmployeeDocuments(this IEnumerable<EmployeeDto> employeeDtos)
        {
            return employeeDtos.Select(x => x.ToEmployeeDocument());
        }
    }
}
