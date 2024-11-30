using System.Diagnostics.CodeAnalysis;
using API.Errors;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.Audits;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specification.Audits;
using Core.Specification.Audits.SpecParams;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
        [ExcludeFromCodeCoverage]

    [Route("api/[controller]")]
    public class AuditController : BaseApiController
    {
        private readonly IGenericRepository<Audit> _genericAudit;
        private readonly IMapper _mapper;

        public AuditController(IGenericRepository<Audit> genericAudit, IMapper mapper)
        {
            _genericAudit = genericAudit;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationWithReadOnyList<AuditReturnDto>>> GetAudits(
            [FromQuery] AuditSpecParams paramsQuery)
        {
            var spec = new AuditGetAllByFilterSpecification(paramsQuery);
            var audits = await _genericAudit.ListReadOnlyListAsync(spec);
            
            var countSpec = new AuditGetAllCountByFilterSpecification(paramsQuery);
            var totalItems = await _genericAudit.CountAsync(countSpec);


            var data = _mapper.Map<IReadOnlyList<AuditReturnDto>>(audits);

            return Ok(new PaginationWithReadOnyList<AuditReturnDto>(paramsQuery.PageIndex,
                paramsQuery.PageSize, totalItems, data));
        }

    }
}
