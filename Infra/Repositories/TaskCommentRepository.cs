using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Core.Interfaces.Repositories.TaskComments;
using Dapper;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
      [ExcludeFromCodeCoverage]

    public class TaskCommentRepository : GenericRepository<TaskComment>, ITaskCommentRepository
    {
        private readonly SampleTaskFlowContext _context;

        public TaskCommentRepository(SampleTaskFlowContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
