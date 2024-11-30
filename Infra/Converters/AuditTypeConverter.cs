using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Converters
{
        [ExcludeFromCodeCoverage]
    public class AuditTypeConverter : ValueConverter<EAuditType, string>
    {
        public AuditTypeConverter() :
        base(
        v => v.ToString(),
        v => (EAuditType)Enum.Parse(typeof(EAuditType), v))
        {

        }
    }
}