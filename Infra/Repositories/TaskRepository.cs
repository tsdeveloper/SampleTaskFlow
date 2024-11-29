using Core.Interfaces.Repositories.Tasks;
using Infra.Data;

namespace Infra.Repositories
{
    public class TaskRepository : GenericRepository<Core.Entities.Task>, ITaskRepository
    {
        private readonly SampleTaskFlowContext _context;

        public TaskRepository(SampleTaskFlowContext context) : base(context)
        {
            _context=context;
        }

    }
}
