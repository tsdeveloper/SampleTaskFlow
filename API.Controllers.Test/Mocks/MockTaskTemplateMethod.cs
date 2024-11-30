using System.Diagnostics.CodeAnalysis;
using Core.Interfaces;
using Core.Interfaces.Repositories.Tasks;
using Core.Specification.Tasks;
using Infra.Repositories;
using Infra.TemplateMethod;
using Moq;

namespace API.Controllers.Test.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class MockTaskTemplateMethod
    {
        public static Mock<TaskTemplateMethod> Validate(this Mock<TaskTemplateMethod> mock, bool @return)
        {
            mock.Setup(m => m.Validate(It.IsAny<Core.Entities.Project>())).Returns(@return);
            return mock;
        }

        public static Mock<TaskTemplateMethod> CanAddNewTaskProjectWithLimit(this Mock<TaskTemplateMethod> mock, bool @return)
        {
            mock.Setup(m => m.CanAddNewTaskProjectWithLimit(It.IsAny<Core.Entities.Project>())).Returns(@return);
            return mock;
        }
    }
}
