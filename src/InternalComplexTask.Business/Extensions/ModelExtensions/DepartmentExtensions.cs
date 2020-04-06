using System.Collections.Generic;
using System.Linq;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Data.Contracts.Entities;

namespace InternalComplexTask.Business.Extensions.ModelExtensions
{
    public static class DepartmentExtensions
    {
        public static DepartmentDto ToDto(this Department entity) =>
            new DepartmentDto
            {
                Id = entity.Id,
                Name = entity.Name
            };

        public static IEnumerable<DepartmentDto> ToDtos(this IEnumerable<Department> entities) =>
            entities.Select(x => x.ToDto());

        public static Department ToEntity(this DepartmentDto dto) =>
            new Department
            {
                Id = dto.Id,
                Name = dto.Name
            };

        public static IEnumerable<Department> ToEntities(this IEnumerable<DepartmentDto> dtos) =>
            dtos.Select(x => x.ToEntity());
    }
}
