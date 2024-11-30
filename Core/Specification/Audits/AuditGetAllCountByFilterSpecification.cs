using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Core.Specification.Audits.SpecParams;

namespace Core.Specification.Audits
{
        [ExcludeFromCodeCoverage]
    public class AuditGetAllCountByFilterSpecification : BaseSpecification<Audit>
    {
        public AuditGetAllCountByFilterSpecification(AuditSpecParams specParams)
        : base(x =>
              (specParams.Search == null || x.TableName.Contains(specParams.Search))
              && (specParams.UserId == null || x.UserId.Equals(specParams.UserId))
              && (specParams.Type == null || x.Type.Equals(specParams.Type))
        )
        {          

        }
    }
}
