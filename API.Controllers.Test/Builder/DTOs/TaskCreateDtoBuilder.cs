using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Tasks;

namespace API.Controllers.Test.Builder.DTOs
{
    [ExcludeFromCodeCoverage]
    public class TaskCreateDtoBuilder : BaseBuilder<TaskCreateDto>
    {
        public TaskCreateDtoBuilder Default()
        {
            _instance.Name = "Task1";
            _instance.Description = "Task1";
            _instance.ProjectId = 1;
            _instance.Priority = Core.Entities.ETaskPriorityType.MIDDLE;
            _instance.Status = Core.Entities.ETaskStatusType.COMPLETED;
            return this;

        }
    }
}
