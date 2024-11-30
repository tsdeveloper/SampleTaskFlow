using System.Diagnostics.CodeAnalysis;
using Core.DTOs;
using Core.DTOs.Tasks;

namespace API.Controllers.Test.Builder.DTOs
{
    [ExcludeFromCodeCoverage]
    public class GenericResponseReturnDeleteTaskBuilder : BaseBuilder<GenericResponse<bool>>
    {
        public GenericResponseReturnDeleteTaskBuilder Default()
        {
            _instance.Error = null;
            _instance.Data = true;
            return this;

        }
    }
}
