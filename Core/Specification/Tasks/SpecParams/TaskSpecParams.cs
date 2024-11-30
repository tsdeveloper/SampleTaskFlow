using System.Diagnostics.CodeAnalysis;
using Core.Entities;

namespace Core.Specification.Tasks.SpecParams
{
        [ExcludeFromCodeCoverage]
    public class TaskSpecParams : BaseSpecParams
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public ETaskPriorityType? Priority { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? EnableIncludeProject { get; set; }
        public bool? EnableIncludeTaskComment { get; set; }
    }
}
