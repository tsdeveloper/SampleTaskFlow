using System.Diagnostics.CodeAnalysis;

namespace API.Errors
{
    [ExcludeFromCodeCoverage]
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}