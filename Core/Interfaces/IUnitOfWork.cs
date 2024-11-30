using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
      IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
      Task BeginTransactionAsync();

      Task<GenericResponse<int>> SaveChangesAsync();
      // Task<GenericResponse<int>> BeforeSaveChanges<TEntity>(int primaryKey) where TEntity : class;
      Task<GenericResponse<int>> BeforeSaveChanges();

      Task CommitAsync();

      Task RollbackAsync();
    }
}
