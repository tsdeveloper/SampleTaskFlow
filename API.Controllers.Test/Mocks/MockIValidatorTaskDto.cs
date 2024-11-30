using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Tasks;
using Core.Interfaces;
using Core.Specification.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Infra.Repositories;
using Moq;

namespace API.Controllers.Test.Mocks
{
    [ExcludeFromCodeCoverage]

    public static class MockIValidatorTaskDto
    {
        public static Mock<IValidator<TaskCreateDto>> MockValidateTaskCreateDto(this Mock<IValidator<TaskCreateDto>> mock, ValidationResult @return)
        {
            mock.Setup(m => m.Validate(It.IsAny<TaskCreateDto>())).Returns(@return);
            return mock;
        }

        public static Mock<IValidator<TaskUpdateDto>> MockTaskUpdateDto(this Mock<IValidator<TaskUpdateDto>> mock, ValidationResult @return)
        {
            mock.Setup(m => m.Validate(It.IsAny<TaskUpdateDto>())).Returns(@return);
            return mock;
        }
    }
}
