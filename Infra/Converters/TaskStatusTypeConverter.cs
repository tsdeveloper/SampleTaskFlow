using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Converters;

public class TaskStatusTypeConverter : ValueConverter<ETaskStatusType, string >
{
   public TaskStatusTypeConverter()
   : base(
    v => v.ToString(),
    v => (ETaskStatusType)Enum.Parse(typeof(ETaskStatusType), v))
{}
}
