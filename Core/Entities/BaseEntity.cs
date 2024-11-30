using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities
{
        [ExcludeFromCodeCoverage]

    public class BaseEntity
    {
      public DateTime CreatedAt { get; set; }
      public DateTime? UpdatedAt { get; set; }
      public bool IsDeleted { get; set; }

          public void UpdateDate(EntityState state)
    {
        switch (state)
        {
            case EntityState.Added:
            case EntityState.Detached:
                CreatedAt = DateTime.Now;

                break;
            case EntityState.Modified:
                UpdatedAt = DateTime.Now;

                break;
        }
    }
    }
}
