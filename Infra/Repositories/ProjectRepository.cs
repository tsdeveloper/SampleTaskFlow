using Core.Entities;
using Core.Interfaces.Repositories.Projects;
using Infra.Data;

namespace Infra.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly SampleTaskFlowContext _context;

        public ProjectRepository(SampleTaskFlowContext context) : base(context)
        {
            _context=context;
        }
    }
}
