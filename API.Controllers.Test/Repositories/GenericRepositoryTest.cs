using API.Controllers.Test.Builder.Entities;
using API.Controllers.Test.Mocks;
using API.Controllers.Test.Service;
using Moq;
using Shouldly;
using API.Controllers.Test.Builder.DTOs;
using Microsoft.AspNetCore.Mvc;
using Core.DTOs.Tasks;
using Core.Specification.Tasks;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Interfaces.Repositories.Tasks;
using Core.Interfaces.Services.Tasks;
using Infra.Data;
using Core.Specification;

namespace API.Controllers.Test.Controllers
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options) { }

    }
    public class GenericRepositoryTest : BaseTestService
    {
        private readonly BaseTestService _baseTestService;

        public GenericRepositoryTest()
        {

        }

        // [Fact]
        // public async Task Step_01_AdicionaEntidadeNoContext_QuandoRetornadoSucesso()
        // {
        //     var genericResponse = new GenericResponseReturnDtoBuilder().Default().Build();

        //     _mockTaskRepository.Add();
        //     // _mockGenericRepository.Add();
        //     _genericMockTask.MockAdd();
        //     _genericMockTask.MockGetEntityWithSpec(new TaskBuilder().Default().Build());
        //     // _repoMockTaskTemplateMethod.Validate(true);
        //     // _repoMockTaskTemplateMethod.CanAddNewTaskProjectWithLimit(true);
        //     // _repoMockProjectValidateLimit.Validate(true);
        //     // _repoMockProjectValidateLimit.CanAddNewTaskProjectWithLimit(true);
        //     _repoMockUnitOfWork.MockSaveChangesAsync(genericResponse);
        //     _repoMockUnitOfWork.MockCommitAsync();

        //     var request = new TaskBuilder().Default().Build();

        //     var resultMapperTask = new TaskReturnDtoBuilder().Default().Build();

        //     await _serviceTask.CreateTaskAsync(request);
        //     _mockTaskRepository.Verify(x => x.Add(It.IsAny<Core.Entities.Task>()), Times.Once);
        // }

        // [Fact]
        // public async Task IGenericRepositoryAddAsync_CallsMethodOnce()
        // {
        //     // Arrange
        //     var mockRepository = new Mock<IGenericRepository<Core.Entities.Task>>();

        //     var request = new TaskBuilder().Default().Build();

        //     // Configurar o comportamento do método `AddAsync`
        //     mockRepository
        //         .Setup(repo => repo.GetEntityWithSpec(It.IsAny<ISpecification<Core.Entities.Task>>()))
        //         .ReturnsAsync(request);

        //     // Act
        //     mockRepository.Object.Add(request);

        //     // Assert
        //     mockRepository.Verify(repo => repo.Add(It.Is<Core.Entities.Task>(p => p.TaskId == 1)), Times.Once);
        // }

        [Fact]
        public async Task IGenericRepositoryAddAsync_CallsMethodOnce()
        {
            var request = new TaskBuilder().Default().Build();
            var mockRepository = new Mock<ITaskRepository>();
            var context = DbContextMock.GetInMemoryContext();

            mockRepository.Setup(repo => repo.Add(It.IsAny<Core.Entities.Task>()));

            var repository = mockRepository.Object;

            repository.Add(request);

            var repositoryGeneric = new GenericRepository<Core.Entities.Task>(context);
            repositoryGeneric.Add(request);

            mockRepository.Verify(repo => repo.Add(request), Times.Once);

        }
        [Fact]
        public async Task IGenericRepositoryCountAsync_CallsMethodOnce()
        {
           var request = new TaskBuilder().Default().Build();
            var mockRepository = new Mock<ITaskRepository>();
            var context = DbContextMock.GetInMemoryContext();

            mockRepository.Setup(repo => repo.CountAsync(It.IsAny<TaskGetAllCountByFilterSpecification>()))
            .ReturnsAsync(10);

            var repository = mockRepository.Object;

            var spec = new TaskGetAllCountByFilterSpecification( new Core.Specification.Tasks.SpecParams.TaskSpecParams ());

            var result = await repository.CountAsync(spec);

            var repositoryGeneric = new GenericRepository<Core.Entities.Task>(context);
           await  repositoryGeneric.CountAsync(spec);

            mockRepository.Verify(repo => repo.CountAsync(spec), Times.Once);
        }

        [Fact]
        public async Task IGenericRepositoryListAllAsync_CallsMethodOnce()
        {
           var request = new TaskBuilder().Default().BuildList();
            var mockRepository = new Mock<ITaskRepository>();
            var context = DbContextMock.GetInMemoryContext();

            mockRepository.Setup(repo => repo.ListAllAsync(It.IsAny<TaskGetAllCountByFilterSpecification>()))
            .ReturnsAsync(request);

            var repository = mockRepository.Object;

            var spec = new TaskGetAllCountByFilterSpecification( new Core.Specification.Tasks.SpecParams.TaskSpecParams ());

            var result = await repository.ListAllAsync(spec);

            var repositoryGeneric = new GenericRepository<Core.Entities.Task>(context);
           await  repositoryGeneric.ListAllAsync(spec);

            mockRepository.Verify(repo => repo.ListAllAsync(spec), Times.Once);
        }


    }
}
