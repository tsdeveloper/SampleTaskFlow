using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Projects;
using Core.DTOs.TaskComments;
using Core.Entities;

namespace Core.DTOs.Tasks
{
    [ExcludeFromCodeCoverage]

    public class TaskReturnDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public ETaskPriorityType Priority { get; set; }
        public ETaskStatusType Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
    [ExcludeFromCodeCoverage]

        public class TaskFullReturnDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public ETaskPriorityType Priority { get; set; }
        public ETaskStatusType Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectReturnDto Project { get; set; }
        public IReadOnlyList<TaskCommentReturnDto> TaskCommentList { get; set; } = new List<TaskCommentReturnDto>();

    }
    [ExcludeFromCodeCoverage]

    public class TaskCreateDto
    {
        public int ProjectId { get; set; }
        public ETaskPriorityType Priority { get; set; } = ETaskPriorityType.LOW;
        public ETaskStatusType Status { get; set; } = ETaskStatusType.NOSTARTING;

        public string Name { get; set; }
        public string Description { get; set; }
    }
    [ExcludeFromCodeCoverage]

    public class TaskUpdateDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public ETaskPriorityType Priority { get; set; } = ETaskPriorityType.LOW;
        public ETaskStatusType Status { get; set; } = ETaskStatusType.NOSTARTING;

        public string Name { get; set; }
        public string Description { get; set; }
    }

}