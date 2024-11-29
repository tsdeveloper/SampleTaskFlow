using Core.Entities;
using Core.Interfaces.Repositories.TaskComments;
using Infra.Data;

namespace Infra.Repositories
{
    public class TaskCommentRepository : GenericRepository<TaskComment>, ITaskCommentRepository
    {
        private readonly SampleTaskFlowContext _context;

        public TaskCommentRepository(SampleTaskFlowContext context) : base(context)
        {
            _context=context;
        }
    }
}
