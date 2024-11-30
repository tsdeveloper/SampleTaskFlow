using System.Diagnostics.CodeAnalysis;

namespace Core.DTOs
{
    [ExcludeFromCodeCoverage]

    public class GenericResponse<T>
    {
        public MessageResponse Error { get; set; }
        public T Data { get; set; }
    }
    
    [ExcludeFromCodeCoverage]
    public class MessageResponse
    {
        public string Message { get; set; }
        public int Status { get; set; }
    }
}