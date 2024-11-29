using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Converters;

public class ProjectStatusTypeConverter : ValueConverter<EProjectStatusType, string >
{
   public ProjectStatusTypeConverter()
   : base(
    v => v.ToString(),
    v => (EProjectStatusType)Enum.Parse(typeof(EProjectStatusType), v))
{}
}
