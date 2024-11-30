using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Tasks;
using FluentValidation;

namespace Core.Validators.Tasks
{    
    [ExcludeFromCodeCoverage]
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Description).NotEmpty().NotNull().MaximumLength(200).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Priority).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.ProjectId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
    [ExcludeFromCodeCoverage]
    public class TaskUpdateDtoValidator : AbstractValidator<TaskUpdateDto>
    {
        public TaskUpdateDtoValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(100).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Description).NotEmpty().NotNull().MaximumLength(200).WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Priority).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Status).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.ProjectId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}