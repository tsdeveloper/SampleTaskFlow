namespace Core.Entities
{
    public enum ETaskStatusType
    {
        NOSTARTING,
        STARTING,
        COMPLETED,
        CANCELED,
    }
    public enum ETaskPriorityType
    {
        LOW,
        MIDDLE,
        HIGH,
    }

    public class Task : BaseEntity
    {
        public Task()
        {
            
        }
        public Task(int taskId)
        {
            TaskId = taskId;
        }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public ETaskPriorityType TaskPriority { get; set; }
        public ETaskStatusType TaskStatus { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Project Project { get; set; }
        public ICollection<TaskComment> TaskCommentList { get; set; } = new List<TaskComment>();

    }
}
