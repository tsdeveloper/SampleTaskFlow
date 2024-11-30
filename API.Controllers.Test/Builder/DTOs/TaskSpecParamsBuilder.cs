using System.Diagnostics.CodeAnalysis;
using Core.DTOs;
using Core.DTOs.Tasks;
using Core.Specification.Tasks.SpecParams;

namespace API.Controllers.Test.Builder.DTOs
{
    [ExcludeFromCodeCoverage]
    public class TaskSpecParamsBuilder : BaseBuilder<TaskSpecParams>
    {
        public TaskSpecParamsBuilder Default()
        {
            _instance.Id = 1;
            _instance.Name = "Task1";
            return this;

        }
    }
}
