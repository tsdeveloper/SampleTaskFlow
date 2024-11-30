using System.Diagnostics.CodeAnalysis;
using Core.Entities;

namespace Core.Specification.Projects.SpecParams
{
        [ExcludeFromCodeCoverage]
    public class ProjectSpecParams : BaseSpecParams
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? UserId { get; set; }
        public EProjectStatusType? Status { get; set; }
        public bool? EnabledIncludeTasks { get; set; }
    }
}
