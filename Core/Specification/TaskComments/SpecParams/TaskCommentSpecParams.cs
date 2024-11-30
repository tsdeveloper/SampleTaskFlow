using System.Diagnostics.CodeAnalysis;

namespace Core.Specification.TaskComments.SpecParams
{
        [ExcludeFromCodeCoverage]
    public class TaskCommentSpecParams : BaseSpecParams
    {
        public int? Id { get; set; }
        public int? TaskId { get; set; }
        public string? Description { get; set; }
        public bool? EnabledIncludeTask { get; set; }
    }
}
