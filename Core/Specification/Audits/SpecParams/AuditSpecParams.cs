using System.Diagnostics.CodeAnalysis;
using Core.Entities;

namespace Core.Specification.Audits.SpecParams
{
        [ExcludeFromCodeCoverage]
        public class AuditSpecParams : BaseSpecParams
    {
        public EAuditType? Type { get; set; }
        public int? UserId { get; set; }
    }
}
