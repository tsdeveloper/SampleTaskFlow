using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
        [ExcludeFromCodeCoverage]

    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}