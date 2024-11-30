using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infra.Data;


public class SampleTaskFlowContext : DbContext
{
  public SampleTaskFlowContext(DbContextOptions<SampleTaskFlowContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder m)
  {
    base.OnModelCreating(m);
    m.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}
    [ExcludeFromCodeCoverage]

public static class SampleTaskFlowContextExtension
{
  public static void BeforeSaveChanges(this SampleTaskFlowContext sampleTaskFlowContext)
  {
    try
    {

      Core.Entities.Audit auditAdd = new Core.Entities.Audit();
      sampleTaskFlowContext.ChangeTracker.DetectChanges();

      // var entityEntries = ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);

      var modifiedEntries = sampleTaskFlowContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
      foreach (var entity in modifiedEntries)
      {
        foreach (var propName in entity.CurrentValues.Properties)
        {
          var current = entity.CurrentValues[propName.Name];
          var original = entity.OriginalValues[propName.Name];
        }
      }
    }

    catch (Exception ex)
    {
      Serilog.Log.Error(ex, "Error saving audit");
    }
  }
}