using Core.DTOs.Autors;
using FluentValidation;

namespace Core.Validators.TaskComments
{
    public class TaskCommentCreateDtoValidator : AbstractValidator<TaskCommentCreateDto>
    {
        public TaskCommentCreateDtoValidator()
        {
            RuleFor(c => c.TaskId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Description).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
        }
    }

    public class TaskCommentUpdateDtoValidator : AbstractValidator<TaskCommentUpdateDto>
    {
        public TaskCommentUpdateDtoValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.TaskId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Description).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}