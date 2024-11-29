using System.Reflection;
using Microsoft.EntityFrameworkCore;
namespace Infra.Data
{
    public class SampleTaskFlowContext : DbContext
    {
      public SampleTaskFlowContext(DbContextOptions<SampleTaskFlowContext> options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder m)
      {
        base.OnModelCreating(m);
        m.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }
    }
}