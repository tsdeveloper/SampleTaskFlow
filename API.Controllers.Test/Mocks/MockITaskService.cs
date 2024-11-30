using System.Diagnostics.CodeAnalysis;
using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Services.Tasks;
using Core.Specification.Tasks;
using Infra.Repositories;
using Moq;

namespace API.Controllers.Test.Mocks
{
    [ExcludeFromCodeCoverage]

    public static class MockITaskService
    {
        public static Mock<ITaskService> MockCreateTaskAsync(this Mock<ITaskService> mock, GenericResponse<Core.Entities.Task> @return)
        {
            mock.Setup(m => m.CreateTaskAsync(It.IsAny<Core.Entities.Task>())).ReturnsAsync(@return);
            return mock;
        }

        public static Mock<ITaskService> MockUpdateTaskAsync(this Mock<ITaskService> mock, GenericResponse<Core.Entities.Task> @return)
        {
            mock.Setup(m => m.UpdateTaskAsync(It.IsAny<int>(), It.IsAny<Core.Entities.Task>())).ReturnsAsync(@return);
            return mock;
        }

        public static Mock<ITaskService> MockExcludeTaskAsync(this Mock<ITaskService> mock, GenericResponse<bool> @return)
        {
            mock.Setup(m => m.ExcludeTaskAsync(It.IsAny<int>())).ReturnsAsync(@return);
            return mock;
        }
    }
}
