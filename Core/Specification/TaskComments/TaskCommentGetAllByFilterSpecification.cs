using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Core.Specification.TaskComments.SpecParams;

namespace Core.Specification.TaskComments
{
        [ExcludeFromCodeCoverage]
    public class TaskCommentGetAllByFilterSpecification : BaseSpecification<TaskComment>
    {
        public TaskCommentGetAllByFilterSpecification(TaskCommentSpecParams specParams)
        : base(x =>
              (specParams.Id == null || x.TaskId.Equals(specParams.Id))
              && (specParams.Search == null || x.TaskCommentDescription.Contains(specParams.Search))
              && (specParams.TaskId == null || x.TaskId.Equals(specParams.TaskId))
        )
        {
            AddOrderby(x => x.TaskId);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrWhiteSpace(specParams.Sort))
            {
                switch (specParams)
                {
                    case TaskCommentSpecParams p when p.Sort.Equals("desc"):
                        AddOrderByDescending(p => p.TaskId);
                        break;
                    default:
                        AddOrderby(p => p.TaskId);
                        break;
                }
            }

        }
    }
}
