using System.Diagnostics.CodeAnalysis;
using Core.Interfaces;
using Core.Specification.Tasks;
using Infra.Repositories;
using Moq;

namespace API.Controllers.Test.Mocks
{
        [ExcludeFromCodeCoverage]

    public static class MockGenericRepository
    {
        public static Mock<GenericRepository<Core.Entities.Task>> Add(this Mock<GenericRepository<Core.Entities.Task>> mock)
        {
            mock.Setup(m => m.Add(It.IsAny<Core.Entities.Task>()));
            return mock;
        }
    }
}
