using System.Reflection;
using API.Errors;
using Core.Interfaces;
using Core.Interfaces.Repositories.Projects;
using Core.Interfaces.Repositories.TaskComments;
using Core.Interfaces.Repositories.Tasks;
using Core.Interfaces.Services.Projects;
using Core.Interfaces.Services.TaskComments;
using Core.Interfaces.Services.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using FluentValidation;
using Infra.Data;
using Infra.Repositories;
using Infra.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            //var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
            //var wkHtmlToPdfPath = Path.Combine(Environment.CurrentDirectory, $"wkhtmltox\\v0.12.4\\{architectureFolder}\\libwkhtmltox");
            //CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            var IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            var connectionString = IsDevelopment
           ? configuration.GetConnectionString("DEV-DOCKER-SQLSERVER")
           : configuration.GetConnectionString("PROD-DOCKER-SQLSERVER");

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<SampleTaskFlowContext>(options =>
                options.UseSqlServer(connectionString, m => m.MigrationsAssembly("Infra")));

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            #endregion

            #region Services
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskCommentService, TaskCommentService>();
            services.AddScoped<IProjectService, ProjectService>();

            #endregion

            services.Configure<ApiBehaviorOptions>(o =>
                    {
                        o.InvalidModelStateResponseFactory = actionConext =>
                        {
                            var errors = actionConext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray();
                            var errorResponse = new ApliValidationErrorResponse
                            {
                                Errors = errors
                            };

                            return new BadRequestObjectResult(errorResponse);
                        };
                    });

            services.AddValidatorsFromAssembly(Assembly.Load("Core"));

            return services;
        }

    }
}