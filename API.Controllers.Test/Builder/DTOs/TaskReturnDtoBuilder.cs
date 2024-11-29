using Core.DTOs.Tasks;

namespace API.Controllers.Test.Builder.DTOs
{
    public class TaskReturnDtoBuilder : BaseBuilder<TaskReturnDto>
    {
        public TaskReturnDtoBuilder Default()
        {
             _instance.Id = 1;
            _instance.Name = "Task1";
            _instance.Description = "Task1";
            _instance.ProjectId = 1;
            _instance.Priority = Core.Entities.ETaskPriorityType.MIDDLE;
            _instance.Status = Core.Entities.ETaskStatusType.COMPLETED;
            return this;

        }
    }
}
