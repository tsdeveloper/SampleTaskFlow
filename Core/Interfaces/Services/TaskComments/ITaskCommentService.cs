using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Services.TaskComments
{
    public interface ITaskCommentService
    {
        Task<GenericResponse<TaskComment>> CreateTaskCommentAsync(TaskComment entity);
        Task<GenericResponse<TaskComment>> UpdateTaskCommentAsync(int id, TaskComment entity);
        Task<GenericResponse<bool>> ExcludeTaskCommentAsync(int id);
    }
}
