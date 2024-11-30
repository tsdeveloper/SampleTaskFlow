using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Core.Specification.Projects.SpecParams;

namespace Core.Specification.Projects
{
        [ExcludeFromCodeCoverage]
    public class ProjectGetAllByFilterSpecification : BaseSpecification<Project>
    {
        public ProjectGetAllByFilterSpecification(ProjectSpecParams specParams)
        : base(x =>
              (specParams.Id == null || x.ProjectId.Equals(specParams.Id))
              && (specParams.Search == null || x.ProjectName.Contains(specParams.Search))
              && (specParams.UserId == null || x.ProjectUserId.Equals(specParams.UserId))
              && (specParams.Status == null || x.ProjectStatus.Equals(specParams.Status))
        )
        {
            AddOrderby(x => x.ProjectName);

            if (specParams.EnabledIncludeTasks.HasValue && specParams.EnabledIncludeTasks.Value == true)
                AddInclude(x => x.TaskList);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrWhiteSpace(specParams.Sort))
            {
                switch (specParams)
                {
                    case ProjectSpecParams p when p.Sort.Equals("desc"):
                        AddOrderByDescending(p => p.ProjectName);
                        break;
                    default:
                        AddOrderby(p => p.ProjectName);
                        break;
                }
            }

        }
    }
}
