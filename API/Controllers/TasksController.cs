using System.Diagnostics.CodeAnalysis;
using System.Text;
using API.Errors;
using AutoMapper;
using Core.DTOs.Tasks;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories.Projects;
using Core.Interfaces.Repositories.TaskComments;
using Core.Interfaces.Repositories.Tasks;
using Core.Interfaces.Services.Tasks;
using Core.Specification.Tasks;
using Core.Specification.Tasks.SpecParams;
using DinkToPdf.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PugPdf.Core;

namespace API.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    public class TasksController : BaseApiController
    {
        private readonly IGenericRepository<Core.Entities.Task> _genericTask;
        private readonly ITaskService _serviceTask;
        private readonly IMapper _mapper;
        private readonly IValidator<TaskCreateDto> _validatorTaskCreateDto;
        private readonly IValidator<TaskUpdateDto> _validatorTaskUpdateDto;
        private readonly IConverter _converter;
        private readonly ITaskRepository _repoTask;
        private readonly IProjectRepository _repoProject;
        private readonly ITaskCommentRepository _repoTaskComment;

        public TasksController(IGenericRepository<Core.Entities.Task> genericTask,
            ITaskService serviceTask, IMapper mapper, IValidator<TaskCreateDto> validatorTaskCreateDto,
            IValidator<TaskUpdateDto> validatorTaskUpdateDto,
            IConverter converter,
            ITaskRepository repoTask,
            IProjectRepository repoProject, ITaskCommentRepository repoTaskComment)
        {
            _genericTask = genericTask;
            _serviceTask = serviceTask;
            _mapper = mapper;
            _validatorTaskCreateDto = validatorTaskCreateDto;
            _validatorTaskUpdateDto = validatorTaskUpdateDto;
            _converter = converter;
            _repoTask = repoTask;
            _repoProject = repoProject;
            _repoTaskComment = repoTaskComment;
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationWithReadOnyList<TaskReturnDto>>> GetTasks(
            [FromQuery] TaskSpecParams paramsQuery)
        {
            var spec = new TaskGetAllByFilterSpecification(paramsQuery);
            var countSpec = new TaskGetAllCountByFilterSpecification(paramsQuery);
            var totalItems = await _genericTask.CountAsync(countSpec);

            var tasks = await _genericTask.ListReadOnlyListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TaskReturnDto>>(tasks);

            return Ok(new PaginationWithReadOnyList<TaskReturnDto>(paramsQuery.PageIndex,
                paramsQuery.PageSize, totalItems, data));
        }

        [HttpGet("{id:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskReturnDto>> GetDetailsById(int id)
        {
            var spec = new TaskGetAllByFilterSpecification(new TaskSpecParams { Id = id, EnableIncludeProject = true, EnableIncludeTaskComment = true });
            var result = await _genericTask.GetEntityWithSpec(spec);

            var resultMapper = _mapper.Map<TaskReturnDto>(result);

            return Ok(resultMapper);
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskReturnDto>> PostCreateTask(TaskCreateDto dto)
        {
            var validator = _validatorTaskCreateDto.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var task = _mapper.Map<Core.Entities.Task>(dto);
            var result = await _serviceTask.CreateTaskAsync(task);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            var resultDto = _mapper.Map<TaskReturnDto>(task);

            return CreatedAtAction(nameof(GetDetailsById), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskReturnDto>> PutUpdateTask(int id, TaskUpdateDto dto)
        {
            var validator = _validatorTaskUpdateDto.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var task = _mapper.Map<Core.Entities.Task>(dto);
            var result = await _serviceTask.UpdateTaskAsync(id, task);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            return NoContent();

        }

        [HttpDelete("delete/{id:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskReturnDto>> DeleteTaskById(int id)
        {
            var result = await _serviceTask.ExcludeTaskAsync(id);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            return NoContent();

        }

        [HttpGet("get-avg-tasks")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskReturnDto>> GetAvgTasksLastThirtyDays()
        {
            var result = await _repoTask.GetReportAverrageTaskCompletedLastThirtyDaysAsync();

            return Ok(result);
        }

    }
}
