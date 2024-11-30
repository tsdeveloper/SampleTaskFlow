using System.Diagnostics.CodeAnalysis;
using Core.Specification.Tasks.SpecParams;

namespace Core.Specification.Tasks
{
        [ExcludeFromCodeCoverage]
    public class TaskGetAllCountByFilterSpecification : BaseSpecification<Entities.Task>
    {
        public TaskGetAllCountByFilterSpecification(TaskSpecParams specParams)
        : base(x =>
              (specParams.Id == null || x.TaskId.Equals(specParams.Id))
              && (specParams.Search == null || x.TaskName.Contains(specParams.Search))
              && (specParams.Priority == null || x.TaskPriority.Equals(specParams.Priority))
              && (specParams.ProjectId == null || x.ProjectId.Equals(specParams.ProjectId))
        )
        {


        }
    }
}
