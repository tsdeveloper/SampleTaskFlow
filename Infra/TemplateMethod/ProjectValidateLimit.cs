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

    public class ProjectValidateLimit : TaskTemplateMethod
    {
        private readonly AppConfig _optionsAppConfig;

        public ProjectValidateLimit(AppConfig optionsAppConfig) : base(optionsAppConfig)
        {
            _optionsAppConfig = optionsAppConfig;
        }

        public override bool CanAddNewTaskProjectWithLimit(Project project)
        {
            return project.TaskList.Count < _optionsAppConfig.MaxLimitTask;
        }
    }
}