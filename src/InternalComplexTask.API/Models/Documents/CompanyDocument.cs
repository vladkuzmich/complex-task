using FluentValidation;

namespace InternalComplexTask.API.Models.Documents
{
    public class CompanyDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CompanyDocumentValidator : AbstractValidator<CompanyDocument>
    {
        public CompanyDocumentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage($"{nameof(CompanyDocument.Name)} cannot have more than 100 symbols.");
        }
    }
}
