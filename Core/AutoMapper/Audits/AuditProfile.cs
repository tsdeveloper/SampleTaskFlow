using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Audits;
using Core.Entities;

namespace Core.AutoMapper.Audits
{
    [ExcludeFromCodeCoverage]
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
             CreateMap<Audit, AuditReturnDto>()
                ;
        }
    }
}