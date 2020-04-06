using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Data.Contracts.Entities;

namespace InternalComplexTask.Business.Extensions.ModelExtensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto ToDto(this Employee entity) =>
            new EmployeeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Gender = entity.Gender,
                DepartmentId = entity.DepartmentId ?? default,
                ImageUrl = entity?.ImageUrl
            };

        public static IEnumerable<EmployeeDto> ToDtos(this IEnumerable<Employee> entities) =>
            entities.Select(x => x.ToDto());

        public static Employee ToEntity(this EmployeeDto dto) =>
            new Employee
            {
                Id = dto.Id,
                Name = dto.Name,
                Surname = dto.Surname,
                Gender = dto.Gender,
                ImageUrl = dto?.ImageUrl
            };

        public static IEnumerable<Employee> ToEntities(this IEnumerable<EmployeeDto> dtos) =>
            dtos.Select(x => x.ToEntity());
    }
}
