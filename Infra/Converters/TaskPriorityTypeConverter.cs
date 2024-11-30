using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Converters;
    [ExcludeFromCodeCoverage]
public class TaskPriorityTypeConverter : ValueConverter<ETaskPriorityType, string >
{
   public TaskPriorityTypeConverter()
   : base(
    v => v.ToString(),
    v => (ETaskPriorityType)Enum.Parse(typeof(ETaskPriorityType), v))
{}
}
