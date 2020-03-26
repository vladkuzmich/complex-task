using System.Collections.Generic;
using System.Linq;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Data.Contracts.Entities;

namespace InternalComplexTask.Business.Extensions.ModelExtensions
{
    public static class CompanyExtensions
    {
        public static CompanyDto ToDto(this Company entity) =>
            new CompanyDto
            {
                Id = entity.Id,
                Name = entity.Name
            };

        public static IEnumerable<CompanyDto> ToDtos(this IEnumerable<Company> entities) =>
            entities.Select(x => x.ToDto());

        public static Company ToEntity(this CompanyDto dto) =>
            new Company
            {
                Id = dto.Id,
                Name = dto.Name
            };

        public static IEnumerable<Company> ToEntities(this IEnumerable<CompanyDto> dtos) =>
            dtos.Select(x => x.ToEntity());
    }
}
