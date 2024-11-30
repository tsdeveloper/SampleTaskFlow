using System.Diagnostics.CodeAnalysis;
using Core.DTOs.Tasks;

namespace Core.DTOs.TaskComments
{
    [ExcludeFromCodeCoverage]
    public class TaskCommentReturnDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TaskCommentFullReturnDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
        public TaskReturnDto Task { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TaskCommentCreateDto
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TaskCommentUpdateDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
    }
}