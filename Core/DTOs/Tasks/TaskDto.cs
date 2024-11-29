using Core.DTOs.Assuntos;
using Core.DTOs.Autors;
using Core.Entities;

namespace Core.DTOs.Tasks
{
    public class TaskReturnDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public ETaskPriorityType Priority { get; set; }
        public ETaskStatusType Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

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

    public class TaskCreateDto
    {
        public int ProjectId { get; set; }
        public ETaskPriorityType Priority { get; set; }
        public ETaskStatusType Status { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TaskUpdateDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public ETaskPriorityType Priority { get; set; }
        public ETaskStatusType Status { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }

}