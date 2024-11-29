using API.Errors;
using AutoMapper;
using Core.DTOs.Autors;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services.TaskComments;
using Core.Specification.TaskComments;
using Core.Specification.TaskComments.SpecParams;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TaskCommentController : BaseApiController
    {
        private readonly IGenericRepository<TaskComment> _genericTaskComment;
        private readonly ITaskCommentService _serviceTaskComment;
        private readonly IMapper _mapper;
        private readonly IValidator<TaskCommentCreateDto> _validatorTaskCommentCreateDto;
        private readonly IValidator<TaskCommentUpdateDto> _validatorTaskCommentUpdateDto;

        public TaskCommentController(IGenericRepository<TaskComment> genericTaskComment,
         ITaskCommentService serviceTaskComment, 
         IMapper mapper, 
         IValidator<TaskCommentCreateDto> validatorTaskCommentCreateDto, 
         IValidator<TaskCommentUpdateDto> validatorTaskCommentUpdateDto)
        {
            _genericTaskComment = genericTaskComment;
            _serviceTaskComment = serviceTaskComment;
            _mapper = mapper;
            _validatorTaskCommentCreateDto = validatorTaskCommentCreateDto;
            _validatorTaskCommentUpdateDto = validatorTaskCommentUpdateDto;
        }

        [HttpGet("all")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationWithReadOnyList<TaskCommentReturnDto>>> GetTaskComments(
            [FromQuery] TaskCommentSpecParams paramsQuery)
        {
            var spec = new TaskCommentGetAllByFilterSpecification(paramsQuery);
            var countSpec = new TaskCommentGetAllCountByFilterSpecification(paramsQuery);
            var totalItems = await _genericTaskComment.CountAsync(countSpec);

            var taskComments = await _genericTaskComment.ListReadOnlyListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TaskCommentReturnDto>>(taskComments);

            return Ok(new PaginationWithReadOnyList<TaskCommentReturnDto>(paramsQuery.PageIndex,
                paramsQuery.PageSize, totalItems, data));
        }

        [HttpGet("details/{id:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskCommentReturnDto>> GetDetailsById(int id)
        {
            var spec = new TaskCommentGetAllByFilterSpecification(new TaskCommentSpecParams { Id = id });
            var result = await _genericTaskComment.GetEntityWithSpec(spec);

            return Ok(_mapper.Map<TaskCommentReturnDto>(result));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskCommentReturnDto>> PostCreateTaskComment(TaskCommentCreateDto dto)
        {
            var validator = _validatorTaskCommentCreateDto.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var taskComment = _mapper.Map<TaskComment>(dto);
            var result = await _serviceTaskComment.CreateTaskCommentAsync(taskComment);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            var resultDto = _mapper.Map<TaskCommentReturnDto>(taskComment);

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
        public async Task<ActionResult<TaskCommentReturnDto>> PutUpdateTaskComment(int id, TaskCommentUpdateDto dto)
        {
            var validator = _validatorTaskCommentUpdateDto.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var taskComment = _mapper.Map<TaskComment>(dto);
            var result = await _serviceTaskComment.UpdateTaskCommentAsync(id, taskComment);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTaskCommentById(int id)
        {
            var result = await _serviceTaskComment.ExcludeTaskCommentAsync(id);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            return NoContent();

        }
    }
}
