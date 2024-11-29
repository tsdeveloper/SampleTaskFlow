using Core.Interfaces;
using Core.Specification.Tasks;
using Moq;

namespace API.Controllers.Test.Mocks
{
    public static class MockTaskRepository
    {
        public static Mock<IGenericRepository<Core.Entities.Task>> MockGetEntityWithSpec(this Mock<IGenericRepository<Core.Entities.Task>> mock, Core.Entities.Task @return)
        {
            mock.Setup(m => m.GetEntityWithSpec(It.IsAny<TaskGetAllByFilterSpecification>())).ReturnsAsync(@return);
            return mock;
        }
    }
}
