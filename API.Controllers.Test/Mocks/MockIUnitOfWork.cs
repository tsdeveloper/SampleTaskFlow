using System.Diagnostics.CodeAnalysis;
using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories.Tasks;
using Core.Specification.Tasks;
using Infra.Repositories;
using Moq;

namespace API.Controllers.Test.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class MockIUnitOfWork
    {
        public static Mock<IUnitOfWork> MockSaveChangesAsync(this Mock<IUnitOfWork> mock, GenericResponse<int> @return)
        {
            mock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(@return);
            return mock;
        }

        public static Mock<IUnitOfWork> MockCommitAsync(this Mock<IUnitOfWork> mock)
        {
            mock.Setup(m => m.CommitAsync());
            return mock;
        }
    }
}
