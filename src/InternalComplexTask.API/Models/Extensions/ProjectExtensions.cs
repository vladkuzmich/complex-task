using System.Collections.Generic;
using System.Linq;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.API.Models.Extensions
{
    public static class ProjectExtensions
    {
        public static ProjectDocument ToDocument(this ProjectDto dto)
        {
            return new ProjectDocument
            {
                Id = dto.Id,
                Name = dto.Name,
                Budget = dto.Budget
            };
        }

        public static IEnumerable<ProjectDocument> ToDocuments(this IEnumerable<ProjectDto> dtos)
        {
            return dtos.Select(x => x.ToDocument());
        }

        public static ProjectDto ToDto(this ProjectDocument document)
        {
            return new ProjectDto
            {
                Id = document.Id,
                Name = document.Name,
                Budget = document.Budget
            };
        }

        public static IEnumerable<ProjectDto> ToDtos(this IEnumerable<ProjectDocument> documents)
        {
            return documents.Select(x => x.ToDto());
        }
    }
}
