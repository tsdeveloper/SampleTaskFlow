using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Core.Specification.Audits.SpecParams;

namespace Core.Specification.Audits
{
        [ExcludeFromCodeCoverage]
    public class AuditGetAllByFilterSpecification : BaseSpecification<Audit>
    {
        public AuditGetAllByFilterSpecification(AuditSpecParams specParams)
        : base(x =>
              (specParams.Search == null || x.TableName.Contains(specParams.Search))
              && (specParams.UserId == null || x.UserId.Equals(specParams.UserId))
              && (specParams.Type == null || x.Type.Equals(specParams.Type))
        )
        {
            AddOrderby(x => x.DateTime);

            if (specParams.EnabledPagination.HasValue && specParams.EnabledPagination.Value == true)
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrWhiteSpace(specParams.Sort))
            {
                switch (specParams)
                {
                    case AuditSpecParams p when p.Sort.Equals("desc"):
                        AddOrderByDescending(p => p.DateTime);
                        break;
                    default:
                        AddOrderby(p => p.DateTime);
                        break;
                }
            }

        }
    }
}
