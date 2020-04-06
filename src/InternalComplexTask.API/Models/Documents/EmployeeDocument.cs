using FluentValidation;
using InternalComplexTask.Data.Contracts.Entities.Enums;

namespace InternalComplexTask.API.Models.Documents
{
    public class EmployeeDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
    }

    public class EmployeeDocumentValidator : AbstractValidator<EmployeeDocument>
    {
        public EmployeeDocumentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage($"{nameof(EmployeeDocument.Name)} cannot have more than 100 symbols.");

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage($"{nameof(EmployeeDocument.Surname)} cannot have more than 50 symbols.");

            RuleFor(x => x.Gender)
                .IsInEnum();
        }
    }
}
