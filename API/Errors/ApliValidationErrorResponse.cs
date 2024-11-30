using System.Diagnostics.CodeAnalysis;

namespace API.Errors
{
        [ExcludeFromCodeCoverage]

    public class ApliValidationErrorResponse : ApiResponse
    {
        public ApliValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }

    }
}
