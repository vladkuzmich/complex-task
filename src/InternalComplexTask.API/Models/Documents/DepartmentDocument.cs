using FluentValidation;

namespace InternalComplexTask.API.Models.Documents
{
    public class DepartmentDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentDocumentValidator : AbstractValidator<DepartmentDocument>
    {
        public DepartmentDocumentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage($"{nameof(CompanyDocument.Name)} cannot have more than 100 symbols.");
        }
    }
}
