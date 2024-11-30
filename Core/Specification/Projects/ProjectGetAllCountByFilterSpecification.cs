using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Core.Specification.Projects.SpecParams;

namespace Core.Specification.Projects
{
        [ExcludeFromCodeCoverage]
    public class ProjectGetAllCountByFilterSpecification : BaseSpecification<Project>
    {
        public ProjectGetAllCountByFilterSpecification(ProjectSpecParams specParams)
        : base(x =>
            (specParams.Id == null || x.ProjectId.Equals(specParams.Id))
              && (specParams.Search == null || x.ProjectName.Contains(specParams.Search))
              && (specParams.UserId == null || x.ProjectUserId.Equals(specParams.UserId))
              && (specParams.Status == null || x.ProjectStatus.Equals(specParams.Status))
        )
        {

        }
    }
}
