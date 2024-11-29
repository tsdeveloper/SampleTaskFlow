using Core.Specification.Tasks.SpecParams;

namespace Core.Specification.Tasks
{
    public class TaskGetAllByFilterSpecification : BaseSpecification<Entities.Task>
    {
        public TaskGetAllByFilterSpecification(TaskSpecParams specParams)
        : base(x =>
              (specParams.Id == null || x.TaskId.Equals(specParams.Id))
              && (specParams.Search == null || x.TaskName.Contains(specParams.Search))
              && (specParams.Priority == null || x.TaskPriority.Equals(specParams.Priority))
              && (specParams.ProjectId == null || x.ProjectId.Equals(specParams.ProjectId))
        )
        {
            if (specParams.EnableIncludeProject.HasValue && specParams.EnableIncludeProject.Value == true)
                AddInclude(x => x.Project);

            if (specParams.EnableIncludeTaskComment.HasValue && specParams.EnableIncludeTaskComment.Value == true)
                AddInclude(x => x.TaskCommentList);

            AddOrderby(x => x.TaskName);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrWhiteSpace(specParams.Sort))
            {
                switch (specParams)
                {
                    case TaskSpecParams p when p.Sort.Equals("desc"):
                        AddOrderByDescending(p => p.TaskName);
                        break;
                    default:
                        AddOrderby(p => p.TaskName);
                        break;
                }
            }

        }
    }
}
