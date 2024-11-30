using API.Controllers.Test.Builder.Entities;
using API.Controllers.Test.Mocks;
using API.Controllers.Test.Service;
using Moq;
using Shouldly;
using API.Controllers.Test.Builder.DTOs;
using Microsoft.AspNetCore.Mvc;
using Core.DTOs.Tasks;
using Core.Specification.Tasks;
using Core.Helpers;

namespace API.Controllers.Test.Controllers
{
    public class TaskControllerTest : BaseTestService
    {
        private TasksController _taskController;
        private readonly BaseTestService _baseTestService;

        public TaskControllerTest()
        {

            _taskController = new TasksController(_genericMockTask.Object,
            _mockITaskService.Object,
            _repoMockMapper.Object,
            _validatorMockTaskCreateDto.Object,
            _validatorMockTaskUpdateDto.Object,
            _mockTaskRepository.Object,
            _reporMockProject.Object,
            _repoMockTaskComment.Object
            );
        }

        [Fact]
        public async Task Step_01_TaskEncontrado_GetController()
        {
            _genericMockTask.MockGetEntityWithSpec(new TaskBuilder().Default().Build());

            var request = new TaskBuilder().Default().Build();

            var resultMapperTask = new TaskReturnDtoBuilder().Default().Build();

            _repoMockMapper.Setup(mapper => mapper.Map<TaskReturnDto>(It.IsAny<Core.Entities.Task>()))
                        .Returns(resultMapperTask);

            var result = await _taskController.GetDetailsById(request.TaskId);

            var matchResponse = ((OkObjectResult)result.Result).Value as TaskReturnDto;

            matchResponse.ShouldNotBeNull();
            matchResponse.Id.ShouldBeEquivalentTo(request.TaskId);
            _genericMockTask.Verify(x => x.GetEntityWithSpec(It.IsAny<TaskGetAllByFilterSpecification>()), Times.Once);
        }

        [Fact]
        public async Task Step_02_SucessoQuandoRetornarTodasTask_GetController()
        {
            var resultMapperTask = new TaskReturnDtoBuilder().Default().BuildListReadOnly();

            _genericMockTask.MockCountAsync(1);

            _repoMockMapper.Setup(mapper => mapper.Map<IReadOnlyList<TaskReturnDto>>(It.IsAny<IReadOnlyList<Core.Entities.Task>>()))
                        .Returns(resultMapperTask);
            _genericMockTask.MockGetEntityWithSpec(new TaskBuilder().Default().Build());
            _genericMockTask.MockListReadOnlyListAsync(new TaskBuilder().Default().BuildList());

            var request = new TaskSpecParamsBuilder().Default().Build();

            var result = await _taskController.GetTasks(request);

            var matchResponse = ((OkObjectResult)result.Result).Value as PaginationWithReadOnyList<TaskReturnDto>;

            matchResponse.ShouldNotBeNull();
            matchResponse.Data.ShouldBe(resultMapperTask);
            _genericMockTask.Verify(x => x.ListReadOnlyListAsync(It.IsAny<TaskGetAllByFilterSpecification>()), Times.Once);
        }

        [Fact]
        public async Task Step_03_SucessoQuandoRetornarTaskQuandoAtualizadoNoBancoDados_PutUpdateTaskController()
        {
            var builderGenerictResponseEntity = new GenericGenericResponseTaskBuilder().Default().Build();
            var builderEntity = new TaskBuilder().Default().Build();
            var builderUpdateDto = new TaskUpdateDtoBuilder().Default().Build();
            var builderReturnDto = new TaskReturnDtoBuilder().Default().Build();

            _validatorMockTaskUpdateDto.MockValidateTaskUpdateDto(new FluentValidation.Results.ValidationResult());


            _repoMockMapper.Setup(mapper => mapper.Map<Core.Entities.Task>(It.IsAny<Core.DTOs.Tasks.TaskCreateDto>()))
                        .Returns(builderEntity);

            _repoMockMapper.Setup(mapper => mapper.Map<TaskReturnDto>(It.IsAny<Core.Entities.Task>()))
                        .Returns(builderReturnDto);

            _mockITaskService.MockUpdateTaskAsync(builderGenerictResponseEntity);

            var result = await _taskController.PutUpdateTask(builderUpdateDto.Id, builderUpdateDto);

            result.ShouldNotBeNull();
            _mockITaskService.Verify(x => x.UpdateTaskAsync(It.IsAny<int>(),It.IsAny<Core.Entities.Task>()), Times.Once);
        }

        [Fact]
        public async Task Step_04_SucessoQuandoRetornarTaskQuandoInserirNoBancoDados_PostCreateTaskController()
        {
            var builderGenerictResponseEntity = new GenericGenericResponseTaskBuilder().Default().Build();
            var builderEntity = new TaskBuilder().Default().Build();
            var builderCreateDto = new TaskCreateDtoBuilder().Default().Build();
            var builderReturnDto = new TaskReturnDtoBuilder().Default().Build();

            _validatorMockTaskCreateDto.MockValidateTaskCreateDto(new FluentValidation.Results.ValidationResult());


            _repoMockMapper.Setup(mapper => mapper.Map<Core.Entities.Task>(It.IsAny<Core.DTOs.Tasks.TaskCreateDto>()))
                        .Returns(builderEntity);

            _repoMockMapper.Setup(mapper => mapper.Map<TaskReturnDto>(It.IsAny<Core.Entities.Task>()))
                        .Returns(builderReturnDto);

            _mockITaskService.MockCreateTaskAsync(builderGenerictResponseEntity);

            var result = await _taskController.PostCreateTask(builderCreateDto);

            var matchResponse = ((CreatedAtActionResult)result.Result).Value as TaskReturnDto;

            matchResponse.ShouldNotBeNull();
            matchResponse.Name.ShouldBeSameAs(builderCreateDto.Name);
            _mockITaskService.Verify(x => x.CreateTaskAsync(It.IsAny<Core.Entities.Task>()), Times.Once);
        }

        [Fact]
        public async Task Step_05_SucessoQuandoRetornarTaskQuandoExcluirNoBancoDados_DeleteTaskByIdTaskController()
        {
            var builderGenerictResponseEntity = new GenericResponseReturnDeleteTaskBuilder().Default().Build();
            var builderEntity = new TaskBuilder().Default().Build();
            var builderCreateDto = new TaskCreateDtoBuilder().Default().Build();
            var builderReturnDto = new TaskReturnDtoBuilder().Default().Build();

            _validatorMockTaskCreateDto.MockValidateTaskCreateDto(new FluentValidation.Results.ValidationResult());


            _repoMockMapper.Setup(mapper => mapper.Map<Core.Entities.Task>(It.IsAny<Core.DTOs.Tasks.TaskCreateDto>()))
                        .Returns(builderEntity);

            _repoMockMapper.Setup(mapper => mapper.Map<TaskReturnDto>(It.IsAny<Core.Entities.Task>()))
                        .Returns(builderReturnDto);

            _mockITaskService.MockExcludeTaskAsync(builderGenerictResponseEntity);

            var result = await _taskController.DeleteTaskById(builderReturnDto.Id);


            result.ShouldNotBeNull();
            _mockITaskService.Verify(x => x.ExcludeTaskAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
