namespace API.Controllers.Test.Builder.Entities
{
    public class TaskBuilder : BaseBuilder<Core.Entities.Task>
    {
        public TaskBuilder Default()
        {
            _instance.TaskId = 1;
            _instance.TaskName = "Task1";
            _instance.TaskDescription = "Task1";
            _instance.ProjectId = 1;
            _instance.TaskPriority = Core.Entities.ETaskPriorityType.MIDDLE;
            _instance.TaskStatus = Core.Entities.ETaskStatusType.COMPLETED;
            return this;

        }
    }
}
