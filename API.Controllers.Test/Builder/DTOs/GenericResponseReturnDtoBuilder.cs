using System.Diagnostics.CodeAnalysis;
using Core.DTOs;
using Core.DTOs.Tasks;

namespace API.Controllers.Test.Builder.DTOs
{
    [ExcludeFromCodeCoverage]
    public class GenericResponseReturnDtoBuilder : BaseBuilder<GenericResponse<int>>
    {
        public GenericResponseReturnDtoBuilder Default()
        {
            _instance.Error = null;
            _instance.Data = 1;
            return this;

        }
    }
}
