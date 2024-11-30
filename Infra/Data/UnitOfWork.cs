using System.Collections;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Entity.Validation;
using Infra.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Data
{
        [ExcludeFromCodeCoverage]

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleTaskFlowContext _context;
        private IDbContextTransaction? _currentTransaction;
        private string _errorMessage = string.Empty;
        private Hashtable _repositories;

        public UnitOfWork(SampleTaskFlowContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction is not null)
                throw new InvalidOperationException("A transaction has already been started.");
            _currentTransaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            if (_currentTransaction is null)
                throw new InvalidOperationException("A transaction has not been started.");
            try
            {

                //Commits the underlying store transaction
                await _currentTransaction.CommitAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
            catch (DbUpdateException ex2)
            {
                if (_currentTransaction is not null)
                    await _currentTransaction.RollbackAsync();

                _currentTransaction = null;
                throw ex2;
            }
            catch (Exception ex)
            {
                if (_currentTransaction is not null)
                    await _currentTransaction.RollbackAsync();

                _currentTransaction = null;
                throw ex;
            }
        }

        public async Task<GenericResponse<int>> SaveChangesAsync()
        {
            var result = new GenericResponse<int>();

            try
            {
                await BeforeSaveChanges();
                //Calling DbContext Class SaveChanges method               
                result.Data = await _context.SaveChangesAsync();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage = _errorMessage +
                                        $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
                result.Error = new MessageResponse();
                result.Error.Message = _errorMessage;
                result.Error.Status = 500;
            }
            catch (Exception ex)
            {
                result.Error = new MessageResponse();
                result.Error.Message = "Error ao salvar os dados!";
                result.Error.Status = 500;
            }

            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {


            if (_repositories == null) _repositories = new Hashtable();

            _context.ChangeTracker.LazyLoadingEnabled = false;

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task RollbackAsync()
        {
            //Rolls back the underlying store transaction
            await _currentTransaction.RollbackAsync();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            _currentTransaction.Dispose();
        }

        public async Task<GenericResponse<int>> BeforeSaveChanges()
        {
            try
            {
                _context.ChangeTracker.DetectChanges();

                 Core.Entities.Audit auditAdd = new Core.Entities.Audit();
            
            // var entityEntries = ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);

                foreach (var entry in _context.ChangeTracker.Entries())
                {

                    var originalValues = entry.OriginalValues.Clone(); // Return current values.


                    if (entry.Entity is Core.Entities.Audit || entry.State is EntityState.Detached or EntityState.Unchanged)
                        continue;
                   var auditEntry = new Core.Entities.AuditEntry(entry) { TableName = entry.Entity.GetType().Name, UserId = "UserSystem" };
                    foreach (var property in entry.Properties)
                    {
                        var propertyName = property.Metadata.Name;
                        if (property.Metadata.IsPrimaryKey())
                        {
                            int primaryKey = int.Parse(property.CurrentValue.ToString());

                            if (primaryKey < 0)
                            {
                                primaryKey = 1;
                            }
                            auditEntry.KeyValues[propertyName] = primaryKey;
                            continue;
                        }

                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.AuditType = Core.Entities.EAuditType.Create;
                                auditEntry.NewValues[propertyName] = property?.CurrentValue;
                                break;
                            case EntityState.Deleted:
                                auditEntry.AuditType = Core.Entities.EAuditType.Delete;
                                auditEntry.OldValues[propertyName] = property?.OriginalValue;
                                break;
                            case EntityState.Modified:
                                if (property.IsModified)
                                {
                                    auditEntry.ChangedColumns.Add(propertyName);
                                    auditEntry.AuditType = Core.Entities.EAuditType.Update;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                                break;
                        }

                       auditAdd = auditEntry.ToAudit();

                    }
                }

                _context.Set<Core.Entities.Audit>().Add(auditAdd);
            }
            catch (Exception ex)
            {
                await RollbackAsync();
                Serilog.Log.Error(ex, "Error saving audit");
            }
            return new GenericResponse<int> { Data = 1 };
        }
    }
}