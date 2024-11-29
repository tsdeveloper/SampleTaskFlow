using Core.DTOs.Assuntos;
using FluentValidation;

namespace Core.Validators.Projects
{
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.UserId).NotEmpty().NotNull().LessThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.MaxLimitTask).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
        }
    }

    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.UserId).NotEmpty().NotNull().LessThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.MaxLimitTask).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}