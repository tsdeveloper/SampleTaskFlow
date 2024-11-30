using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Converters;
    [ExcludeFromCodeCoverage]
public class ProjectStatusTypeConverter : ValueConverter<EProjectStatusType, string >
{
   public ProjectStatusTypeConverter()
   : base(
    v => v.ToString(),
    v => (EProjectStatusType)Enum.Parse(typeof(EProjectStatusType), v))
{}
}
