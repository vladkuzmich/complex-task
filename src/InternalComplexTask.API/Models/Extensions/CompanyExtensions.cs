using System.Collections.Generic;
using System.Linq;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.API.Models.Extensions
{
    public static class CompanyExtensions
    {
        public static CompanyDocument ToCompanyDocument(this CompanyDto companyDto) =>
            new CompanyDocument
            {
                Id = companyDto.Id,
                Name = companyDto.Name
            };

        public static IEnumerable<CompanyDocument> ToCompanyDocuments(this IEnumerable<CompanyDto> companyDtos) =>
            companyDtos.Select(ToCompanyDocument);

        public static CompanyDto ToCompanyDto(this CompanyDocument companyDocument) =>
            new CompanyDto
            {
                Id = companyDocument.Id,
                Name = companyDocument.Name
            };

        public static IEnumerable<CompanyDto> ToCompanyDtos(this IEnumerable<CompanyDocument> companyDocuments) =>
            companyDocuments.Select(ToCompanyDto);
    }
}
