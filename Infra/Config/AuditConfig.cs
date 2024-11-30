using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infra.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
        [ExcludeFromCodeCoverage]
    public class AuditConfig : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> b)
        {
            b.Property(p => p.UserId).HasColumnType("varchar").HasMaxLength(100);
            b.Property(p => p.Type).HasConversion<AuditTypeConverter>().HasMaxLength(100);
            b.Property(p => p.TableName).HasColumnType("varchar").HasMaxLength(100);
            b.Property(p => p.OldValues).HasColumnType("varchar").IsRequired(false).HasMaxLength(8000);
            b.Property(p => p.NewValues).HasColumnType("varchar").HasMaxLength(8000);
            b.Property(p => p.AffectedColumns).HasColumnType("varchar").IsRequired(false).HasMaxLength(8000);
            b.Property(p => p.PrimaryKey).HasColumnType("varchar").HasMaxLength(8000);
        }
    }
}