using API.Controllers.Test.Builder.Entities;
using API.Controllers.Test.Mocks;
using API.Controllers.Test.Service;
using Moq;
using Shouldly;
using API.Controllers.Test.Builder.DTOs;
using Microsoft.AspNetCore.Mvc;
using Core.DTOs.Tasks;
using Core.Specification.Tasks;

namespace API.Controllers.Test.API
{
    public class TaskControllerTest : BaseTestService
    {
        private TasksController _TaskController;
        private readonly BaseTestService _baseTestService;

        public TaskControllerTest()
        {

            _TaskController = new TasksController(_genericMockTask.Object,
            _serviceTask,
            _repoMockMapper.Object,
            _validatorMockTaskCreateDto.Object,
            _validatorMockTaskUpdateDto.Object,
            _repoMockConverter.Object,
            _reporMockTask.Object,
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

            var result = await _TaskController.GetDetailsById(request.TaskId);

            var matchResponse = ((OkObjectResult)result.Result).Value as TaskReturnDto;

            matchResponse.ShouldNotBeNull();
            matchResponse.Id.ShouldBeEquivalentTo(request.TaskId);
            _genericMockTask.Verify(x => x.GetEntityWithSpec(It.IsAny<TaskGetAllByFilterSpecification>()), Times.Once);
        }
    }
}
