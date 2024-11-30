using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Core.Interfaces.Repositories.Tasks;
using Dapper;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    [ExcludeFromCodeCoverage]

    public class TaskRepository : GenericRepository<Core.Entities.Task>, ITaskRepository
    {
        private readonly SampleTaskFlowContext _context;

        public TaskRepository(SampleTaskFlowContext context) : base(context)
        {
            _context = context;
        }
        public async Task<float> GetReportAverrageTaskCompletedLastThirtyDaysAsync()
        {
            using (var context = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var query = @"
                           SELECT 
                                COUNT(*) / 30.0 AS MediaRegistrosPorDia
                            FROM 
                                Task t 
                            WHERE 
                                t.CreatedAt >= DATEADD(DAY, -30, GETDATE());            
            ";
                var result = await context.QueryFirstOrDefaultAsync<float>(query);

                return result;

            }
        }

    }
}
