namespace Core.Interfaces.Repositories.Tasks
{
    
    public interface ITaskRepository : IGenericRepository<Entities.Task>
    {
                Task<float> GetReportAverrageTaskCompletedLastThirtyDaysAsync();

    }
}
