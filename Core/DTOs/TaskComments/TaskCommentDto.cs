using Core.DTOs.Tasks;

namespace Core.DTOs.Autors
{
    public class TaskCommentReturnDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
    }

    public class TaskCommentFullReturnDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
        public TaskReturnDto Task { get; set; }
    }

    public class TaskCommentCreateDto
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
    }

    public class TaskCommentUpdateDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
    }
}