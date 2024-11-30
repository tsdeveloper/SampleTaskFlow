using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Projects;
using FluentValidation;

namespace Core.Validators.Projects
{
    [ExcludeFromCodeCoverage]
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.UserId).NotEmpty().NotNull().GreaterThanOrEqualTo(1).WithMessage("{PropertyName} is required.");
        }
    }
    [ExcludeFromCodeCoverage]
    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.UserId).NotEmpty().NotNull().GreaterThanOrEqualTo(1).WithMessage("{PropertyName} is required.");
        }
    }
}