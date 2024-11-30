using System.Diagnostics.CodeAnalysis;
using Core.DTOs;
using Core.Entities;

namespace API.Controllers.Test.Builder.DTOs
{
    [ExcludeFromCodeCoverage]
    public class GenericGenericResponseTaskBuilder : BaseBuilder<GenericResponse<Core.Entities.Task>>
    {
        public GenericGenericResponseTaskBuilder Default()
        {
            _instance.Error = null;
            _instance.Data = new Core.Entities.Task { TaskId = 1, TaskName = "Task1"};
            return this;

        }
    }
}
