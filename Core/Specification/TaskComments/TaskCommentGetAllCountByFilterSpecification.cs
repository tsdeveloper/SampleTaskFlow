using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Core.Specification.TaskComments.SpecParams;

namespace Core.Specification.TaskComments
{
        [ExcludeFromCodeCoverage]
    public class TaskCommentGetAllCountByFilterSpecification : BaseSpecification<TaskComment>
    {
        public TaskCommentGetAllCountByFilterSpecification(TaskCommentSpecParams specParams)
        : base(x =>
              (specParams.Id == null || x.TaskId.Equals(specParams.Id))
              && (specParams.Search == null || x.TaskCommentDescription.Contains(specParams.Search))
              && (specParams.TaskId == null || x.TaskId.Equals(specParams.TaskId))
        )
        {
        }
    }
}
