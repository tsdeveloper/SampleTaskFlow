using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Tasks;
using Core.Entities;

namespace Core.DTOs.Projects
{
    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
    public class ProjectCreateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public EProjectStatusType Status { get; set; } = EProjectStatusType.NOSTARTING;
    }

    [ExcludeFromCodeCoverage]
    public class ProjectUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public EProjectStatusType Status { get; set; }
    }
}