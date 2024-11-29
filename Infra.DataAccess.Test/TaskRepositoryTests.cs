using Bogus;
using Core.Entities;
using Infra.Data;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Infra.DataAccess.Test
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private List<Core.Entities.Task> _fakers;
   
        public TaskRepositoryTests()
        {
            
            var fakerId = 1;
           _fakers = new Faker<Core.Entities.Task>()
                    .CustomInstantiator(o => new Core.Entities.Task(fakerId++))
                    .RuleFor(o => o.TaskName, f => f.Commerce.Department())
                    .RuleFor(o => o.TaskDescription, f => f.Commerce.ProductAdjective())
                    .RuleFor(o => o.TaskPriority, f => f.PickRandom<ETaskPriorityType>())
                    .RuleFor(o => o.TaskStatus, f => f.PickRandom<ETaskStatusType>())
                    .RuleFor(o => o.ProjectId, f => 1)
                    .Generate(5);
        }

        [Test]
        public void Step_01_SaveTask_And_CheckExistValueDatabaseInMemory()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SampleTaskFlowContext>()
                            .UseInMemoryDatabase(databaseName: "temp_ebookstore").Options;

            //act
            using (var context = new SampleTaskFlowContext(options))
            {
                foreach (var task in _fakers)
                {
                    var unitOfWork = new UnitOfWork(context);
                    unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                    var repository = new TaskRepository(context);
                    repository.Add(task);
                    unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                    unitOfWork.CommitAsync().ConfigureAwait(false);
                }

            }

            using (var context = new SampleTaskFlowContext(options))
            {
                var task = context.Set<Core.Entities.Task>().FirstOrDefault(x => x.TaskId == 1);
                _fakers.FirstOrDefault().TaskId.ShouldBeEquivalentTo(task.TaskId);
            }
        }
    }
}