using System.Diagnostics.CodeAnalysis;
using Bogus;
using Core.Entities;
using Infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Seed
{
      [ExcludeFromCodeCoverage]

    public class DbInitializer
    {
        public static void InitDb(WebApplication app, bool generateMockSeeds = false)
        {
            using var scope = app.Services.CreateScope();
            SeedData(scope.ServiceProvider.GetService<SampleTaskFlowContext>(), generateMockSeeds);
        }
        private static void SeedData(SampleTaskFlowContext context, bool generateMockSeeds = false)
        {
            context.Database.Migrate();

            if (generateMockSeeds)
            {
                SeedProjectFaker(context);
                SeedTaskFaker(context);
                SeedTaskCommentFaker(context);
            }
        }

        private static void SeedProjectFaker(SampleTaskFlowContext context)
        {

            if (!context.Set<Project>().Any())
            {
                var fakerId = 1;
                var fakers = new Faker<Project>()
                //  .CustomInstantiator(o => new Project(fakerId++))
                    .RuleFor(o => o.ProjectName, f => f.Company.CompanySuffix())
                    .RuleFor(o => o.ProjectStatus, f => f.PickRandom<EProjectStatusType>())
                    .RuleFor(o => o.ProjectUserId, f => f.Random.Int(1, 10))
                    .RuleFor(o => o.ProjectMaxLimitTask, f => 20)
                    .Generate(1);

                context.AddRange(fakers);
                context.SaveChanges();

            }
        }

        private static void SeedTaskCommentFaker(SampleTaskFlowContext context)
        {
            var taskList = context.Set<Core.Entities.Task>().Take(10);

            if (!context.Set<TaskComment>().Any())
            {
                int rangeMin = taskList.Select(x => x.TaskId).Min();
                int rangeMax = taskList.Select(x => x.TaskId).Max();

                var fakerId = 1;
                var fakers = new Faker<TaskComment>()
                //  .CustomInstantiator(o => new TaskComment(fakerId++))
                    .RuleFor(o => o.TaskCommentDescription, f => f.Commerce.Department())
                    .RuleFor(o => o.TaskId, f => f.Random.Int(rangeMin, rangeMax))
                    .Generate(25);

                context.AddRange(fakers);
                context.SaveChanges();

            }
        }
        private static void SeedTaskFaker(SampleTaskFlowContext context)
        {
            var projects = context.Set<Project>().Take(10);
            if (!context.Set<Core.Entities.Task>().Any())
            {
                 int rangeMin = projects.Select(x => x.ProjectId).Min();
                int rangeMax = projects.Select(x => x.ProjectId).Max();
                var fakerId = 1;
                var fakers = new Faker<Core.Entities.Task>()
                //  .CustomInstantiator(o => new Core.Entities.Task(fakerId++))
                    .RuleFor(o => o.CreatedAt, f => f.Date.RecentOffset(20, DateTime.Now.AddDays(-25)).Date)
                    .RuleFor(o => o.TaskName, f => f.Commerce.Department())
                    .RuleFor(o => o.TaskDescription, f => f.Commerce.ProductAdjective())
                    .RuleFor(o => o.TaskPriority, f => f.PickRandom<ETaskPriorityType>())
                    .RuleFor(o => o.TaskStatus, f => f.PickRandom<ETaskStatusType>())
                    .RuleFor(o => o.ProjectId, f => f.Random.Int(rangeMin, rangeMax))
                    .Generate(19);

                context.AddRange(fakers);
                context.SaveChanges();

            }
        }
    }
}

