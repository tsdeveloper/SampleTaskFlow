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
    public static class MockProjectValidateLimit
    {
        public static Mock<ProjectValidateLimit> Validate(this Mock<ProjectValidateLimit> mock, bool @return)
        {
            mock.Setup(m => m.Validate(It.IsAny<Core.Entities.Project>())).Returns(@return);
            return mock;
        }

        public static Mock<ProjectValidateLimit> CanAddNewTaskProjectWithLimit(this Mock<ProjectValidateLimit> mock, bool @return)
        {
            mock.Setup(m => m.CanAddNewTaskProjectWithLimit(It.IsAny<Core.Entities.Project>())).Returns(@return);
            return mock;
        }
    }
}
