using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Services.Projects
{
    public interface IProjectService
    {
        Task<GenericResponse<Project>> CreateProjectAsync(Project entity);
        Task<GenericResponse<Project>> UpdateProjectAsync(int id, Project entity);
        Task<GenericResponse<bool>> ExcludeProjectAsync(int id);
    }
}
