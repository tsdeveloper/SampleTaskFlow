using Core.DTOs.Tasks;
using Core.Entities;

namespace Core.DTOs.Assuntos
{
    public class ProjectReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public EProjectStatusType Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int MaxLimitTask { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProjectFullReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public EProjectStatusType Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int MaxLimitTask { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TaskReturnDto> TaskList { get; set; } = new();
    }

    public class ProjectCreateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public EProjectStatusType Status { get; set; }
        public int MaxLimitTask { get; set; }
    }

    public class ProjectUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public EProjectStatusType Status { get; set; }
        public int MaxLimitTask { get; set; }
    }
}