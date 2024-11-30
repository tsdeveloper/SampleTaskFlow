using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Options;

namespace Infra.TemplateMethod
{
    [ExcludeFromCodeCoverage]

    public abstract class TaskTemplateMethod
    {
        private readonly AppConfig _optionsAppConfig;

        public TaskTemplateMethod(AppConfig optionsAppConfig)
        {
            _optionsAppConfig = optionsAppConfig;
        }
        public bool Validate(Project project)
        {
            return CanAddNewTaskProjectWithLimit(project);
        }

        public abstract bool CanAddNewTaskProjectWithLimit(Project project);
    }
}