using System.Collections.Generic;
using System.Linq;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.API.Models.Extensions
{
    public static class DepartmentExtensions
    {
        public static DepartmentDocument ToDocument(this DepartmentDto dto) =>
            new DepartmentDocument
            {
                Id = dto.Id,
                Name = dto.Name
            };

        public static IEnumerable<DepartmentDocument> ToDocuments(this IEnumerable<DepartmentDto> dtos) =>
            dtos.Select(ToDocument);

        public static DepartmentDto ToDto(this DepartmentDocument companyDocument) =>
            new DepartmentDto
            {
                Id = companyDocument.Id,
                Name = companyDocument.Name
            };

        public static IEnumerable<DepartmentDto> ToDtos(this IEnumerable<DepartmentDocument> departments) =>
            departments.Select(ToDto);
    }
}
