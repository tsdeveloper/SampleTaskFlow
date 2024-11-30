using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Tasks;
using Core.Helpers;
using Core.Interfaces;
using Core.Specification.Tasks;
using Moq;

namespace API.Controllers.Test.Mocks
{
    [ExcludeFromCodeCoverage]

    public static class MockGenericTaskRepository
    {
        public static Mock<IGenericRepository<Core.Entities.Task>> MockGetEntityWithSpec(this Mock<IGenericRepository<Core.Entities.Task>> mock, Core.Entities.Task @return)
        {
            mock.Setup(m => m.GetEntityWithSpec(It.IsAny<TaskGetAllByFilterSpecification>())).ReturnsAsync(@return);
            return mock;
        }
        public static Mock<IGenericRepository<Core.Entities.Task>> MockAdd(this Mock<IGenericRepository<Core.Entities.Task>> mock)
        {
            mock.Setup(m => m.Add(It.IsAny<Core.Entities.Task>()));
            return mock;
        }

        public static Mock<IGenericRepository<Core.Entities.Task>> MockListAllAsync(this Mock<IGenericRepository<Core.Entities.Task>> mock, List<Core.Entities.Task> @return)
        {
            mock.Setup(m => m.ListAllAsync(It.IsAny<TaskGetAllByFilterSpecification>())).ReturnsAsync(@return);
            return mock;
        }

        public static Mock<IGenericRepository<Core.Entities.Task>> MockListReadOnlyListAsync(this Mock<IGenericRepository<Core.Entities.Task>> mock, IReadOnlyList<Core.Entities.Task> @return)
        {
            mock.Setup(m => m.ListReadOnlyListAsync(It.IsAny<TaskGetAllByFilterSpecification>())).ReturnsAsync(@return);
            return mock;
        }

        public static Mock<IGenericRepository<Core.Entities.Task>> MockCountAsync(this Mock<IGenericRepository<Core.Entities.Task>> mock, int @return)
        {
            mock.Setup(m => m.CountAsync(It.IsAny<TaskGetAllByFilterSpecification>())).ReturnsAsync(@return);
            return mock;
        }

    }
}
