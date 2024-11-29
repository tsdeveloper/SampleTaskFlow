using Core.DTOs;

namespace Core.Interfaces.Services.Tasks
{
    public interface ITaskService
    {
      Task<GenericResponse<Entities.Task>> CreateTaskAsync(Entities.Task entity);
      Task<GenericResponse<Entities.Task>> UpdateTaskAsync(int id, Entities.Task entity);
      Task<GenericResponse<bool>> ExcludeTaskAsync(int id);
    }
}
