using System.Diagnostics.CodeAnalysis;
using Core.Interfaces;
using Core.Interfaces.Repositories.Tasks;
using Core.Specification.Tasks;
using Infra.Repositories;
using Moq;

namespace API.Controllers.Test.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class MockTaskRepository
    {
        public static Mock<ITaskRepository> Add(this Mock<ITaskRepository> mock)
        {
            mock.Setup(m => m.Add(It.IsAny<Core.Entities.Task>()));
            return mock;
        }
    }
}
