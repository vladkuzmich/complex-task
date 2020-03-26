using FluentValidation;

namespace InternalComplexTask.API.Models.Documents
{
    public class ProjectDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
    }

    public class ProjectDocumentValidator : AbstractValidator<ProjectDocument>
    {
        public ProjectDocumentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
