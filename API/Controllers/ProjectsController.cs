using System.Diagnostics.CodeAnalysis;
using API.Errors;
using AutoMapper;
using Core.DTOs.Projects;
using Core.DTOs.Tasks;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services.Projects;
using Core.Specification.Projects;
using Core.Specification.Projects.SpecParams;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ExcludeFromCodeCoverage]

    [Route("api/[controller]")]
    public class ProjectsController : BaseApiController
    {
        private readonly IGenericRepository<Project> _genericProject;
        private readonly IProjectService _serviceProject;
        private readonly IMapper _mapper;
        private readonly IValidator<ProjectCreateDto> _validatorProjectCreateDto;
        private readonly IValidator<ProjectUpdateDto> _validatorProjectUpdateDto;

        public ProjectsController(IGenericRepository<Project> genericProject, 
        IProjectService serviceProject, IMapper mapper, 
        IValidator<ProjectCreateDto> validatorProjectCreateDto, 
        IValidator<ProjectUpdateDto> validatorProjectUpdateDto)
        {
            _genericProject = genericProject;
            _serviceProject = serviceProject;
            _mapper = mapper;
            _validatorProjectCreateDto = validatorProjectCreateDto;
            _validatorProjectUpdateDto = validatorProjectUpdateDto;
        }

        [HttpGet("all")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationWithReadOnyList<ProjectReturnDto>>> GetProjects(
            [FromQuery] ProjectSpecParams paramsQuery)
        {
            var spec = new ProjectGetAllByFilterSpecification(paramsQuery);
            var countSpec = new ProjectGetAllCountByFilterSpecification(paramsQuery);
            var totalItems = await _genericProject.CountAsync(countSpec);

            var projects = await _genericProject.ListReadOnlyListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProjectReturnDto>>(projects);

            return Ok(new PaginationWithReadOnyList<ProjectReturnDto>(paramsQuery.PageIndex,
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
        public async Task<ActionResult<ProjectReturnDto>> GetDetailsById(int id)
        {
            var spec = new ProjectGetAllByFilterSpecification(new ProjectSpecParams { Id = id });
            var result = await _genericProject.GetEntityWithSpec(spec);

            return Ok(_mapper.Map<ProjectReturnDto>(result));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProjectReturnDto>> PostCreateProject(ProjectCreateDto dto)
        {
            var validator = _validatorProjectCreateDto.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var project = _mapper.Map<Project>(dto);
            var result = await _serviceProject.CreateProjectAsync(project);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            var resultDto = _mapper.Map<ProjectReturnDto>(project);

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
        public async Task<ActionResult<ProjectReturnDto>> PutUpdateProject(int id, ProjectUpdateDto dto)
        {
            var validator = _validatorProjectUpdateDto.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var result = await _serviceProject.UpdateProjectAsync(id, dto);

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
        public async Task<ActionResult<TaskReturnDto>> DeleteProjectById(int id)
        {
            var result = await _serviceProject.ExcludeProjectAsync(id);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            return NoContent();

        }
    }
}
