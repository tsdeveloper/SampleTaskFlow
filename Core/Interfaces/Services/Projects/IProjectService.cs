using Core.DTOs;
using Core.DTOs.Projects;
using Core.Entities;

namespace Core.Interfaces.Services.Projects
{
    public interface IProjectService
    {
        Task<GenericResponse<Project>> CreateProjectAsync(Project entity);
        Task<GenericResponse<Project>> UpdateProjectAsync(int id, ProjectUpdateDto entity);
        Task<GenericResponse<bool>> ExcludeProjectAsync(int id);
    }
}
