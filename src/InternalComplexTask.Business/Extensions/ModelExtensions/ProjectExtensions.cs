using System.Collections.Generic;
using System.Linq;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Data.Contracts.Entities;

namespace InternalComplexTask.Business.Extensions.ModelExtensions
{
    public static class ProjectExtensions
    {
        public static ProjectDto ToDto(this Project entity)
        {
            return new ProjectDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Budget = entity.Budget
            };
        }

        public static IEnumerable<ProjectDto> ToDtos(this IEnumerable<Project> entities)
        {
            return entities.Select(x => x.ToDto());
        }

        public static Project ToEntity(this ProjectDto projectDto)
        {
            return new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Budget = projectDto.Budget
            };
        }

        public static IEnumerable<Project> ToDtos(this IEnumerable<ProjectDto> dtos)
        {
            return dtos.Select(x => x.ToEntity());
        }
    }
}
