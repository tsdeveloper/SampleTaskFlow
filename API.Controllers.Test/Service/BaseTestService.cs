using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Core.DTOs.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories.Projects;
using Core.Interfaces.Repositories.TaskComments;
using Core.Interfaces.Repositories.Tasks;
using Core.Interfaces.Services.Projects;
using Core.Interfaces.Services.TaskComments;
using Core.Interfaces.Services.Tasks;
using FluentValidation;
using Infra.Data;
using Infra.Repositories;
using Infra.Services;
using Infra.TemplateMethod;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Moq;

namespace API.Controllers.Test.Service
{
    [ExcludeFromCodeCoverage]
    public class BaseTestService : WebApplicationFactory<Program>
    {

        internal Dictionary<string, string> OverrideConfiguration = new();
        private readonly WebApplicationFactory<Program> _factory;

        protected Mock<ITaskCommentRepository> _repoMockTaskComment;
        protected Mock<SampleTaskFlowContext> _contextMockSampleTaskFlowContext;
        protected Mock<IGenericRepository<Core.Entities.Task>> _genericMockTask;
        protected Mock<ITaskRepository> _mockTaskRepository;
        protected Mock<TaskRepository> _repoMockTaskRepository;
        protected Mock<IProjectRepository> _reporMockProject;
        protected Mock<IValidator<TaskCreateDto>> _validatorMockTaskCreateDto;
        protected Mock<IValidator<TaskUpdateDto>> _validatorMockTaskUpdateDto;
        protected Mock<IUnitOfWork> _repoMockUnitOfWork;
        protected Mock<IMapper> _repoMockMapper;
        protected ITaskCommentRepository _repoTaskComment => _repoMockTaskComment.Object;
        protected TaskService _serviceTask;
        protected readonly HttpClient _httpClient;
        protected Mock<IOptions<AppConfig>> _repoMockOptionsAppConfig;
        protected Mock<ProjectValidateLimit> _repoMockProjectValidateLimit;
        protected Mock<TaskTemplateMethod> _repoMockTaskTemplateMethod;
        protected Mock<GenericRepository<Core.Entities.Task>> _mockGenericRepository;
        protected DbContextOptions _contextDbContextOptions => new DbContextOptionsBuilder().Options;
        protected TaskRepository _repoTaskRepository;
        private SampleTaskFlowContext _dbContext;
        private AppConfig _appConfig => new AppConfig { MaxLimitTask = 20 };
        protected GenericRepository<Core.Entities.Task> _repoGenericRepository;
        protected Mock<DbContextOptions<SampleTaskFlowContext>> _mockDbContextOptions;
        protected Mock<ITaskService> _mockITaskService;


        public BaseTestService()
        {
            _httpClient = CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("http://localhost:7001/api")
            });
            LoadApplicationMockServices();
            LoadApplicationServices();
        }

        private void LoadApplicationMockServices()
        {
            _repoMockTaskComment = new Mock<ITaskCommentRepository>();
            _genericMockTask = new Mock<IGenericRepository<Core.Entities.Task>>();
            _validatorMockTaskCreateDto = new Mock<IValidator<TaskCreateDto>>();
            _validatorMockTaskUpdateDto = new Mock<IValidator<TaskUpdateDto>>();
            _repoMockUnitOfWork = new Mock<IUnitOfWork>();
            _repoMockMapper = new Mock<IMapper>();
            _mockTaskRepository = new Mock<ITaskRepository>();
            _reporMockProject = new Mock<IProjectRepository>();
            _repoMockOptionsAppConfig = new Mock<IOptions<AppConfig>>();
            _mockITaskService = new Mock<ITaskService>();

        }

        private void LoadApplicationServices()
        {
            _serviceTask = new TaskService(_repoMockUnitOfWork.Object, _repoMockMapper.Object, _mockTaskRepository.Object, _repoMockOptionsAppConfig.Object);
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _repoTaskRepository = new TaskRepository(_dbContext);
            _repoGenericRepository = new GenericRepository<Core.Entities.Task>(_dbContext);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureAppConfiguration((ctx, builder) =>
            {
                System.Console.WriteLine("configure host");
                var testDir = Path.GetDirectoryName(GetType().Assembly.Location);
                var configLocation = Path.Combine(testDir!, "testsettings.json");

                builder.Sources.Clear();
                builder.AddJsonFile(configLocation);
                builder.AddInMemoryCollection(OverrideConfiguration);

            });
            builder.ConfigureTestServices((services) =>
            {
                // Remove the existing DbContextOptions
                services.RemoveAll(typeof(DbContextOptions<SampleTaskFlowContext>));
                services.RemoveAll(typeof(IGenericRepository<>));
                services.RemoveAll<IUnitOfWork>();
                services.RemoveAll<ITaskCommentRepository>();
                services.RemoveAll<ITaskService>();
                services.RemoveAll<ITaskCommentService>();
                services.RemoveAll<IProjectService>();

                var serviceProvider = services.BuildServiceProvider();
                var scope = serviceProvider.CreateScope();
                // Register a new DBContext that will use our test connection string
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                string? connString = GetConnectionString(config);
                services.AddSqlServer<SampleTaskFlowContext>(connString);

                // Delete the database (if exists) to ensure we start clean
                SampleTaskFlowContext dbContext = CreateDbContext(services);
            });
        }
        private static string? GetConnectionString(IConfiguration config)
        {
            var connString = config.GetConnectionString("DEV-DOCKER-SQLSERVER");
            return connString;
        }

        private static SampleTaskFlowContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SampleTaskFlowContext>();
            return dbContext;
        }

    }

    public static class DbContextMock
    {
        public static SampleTaskFlowContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SampleTaskFlowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Garante que o banco seja único para cada teste
                .Options;

            return new SampleTaskFlowContext(options);
        }
    }

    public class InMemoryDbContextFactory
    {
        public SampleTaskFlowContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SampleTaskFlowContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                            // and also tried using SqlLite approach. But same issue reproduced.
                            .Options;
            var dbContext = new SampleTaskFlowContext(options);

            return dbContext;
        }
    }
}
