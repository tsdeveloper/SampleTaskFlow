using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories.TaskComments;
using Core.Interfaces.Repositories.Tasks;
using Core.Interfaces.Services.TaskComments;
using Core.Interfaces.Services.Tasks;
using Core.Specification.Projects;
using Core.Specification.Projects.SpecParams;
using Core.Specification.Tasks;
using Core.Specification.Tasks.SpecParams;
using Infra.TemplateMethod;
using Microsoft.Extensions.Options;

namespace Infra.Services
{
      [ExcludeFromCodeCoverage]

    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _repoTask;
        private readonly AppConfig _appConfig;
        public TaskService(IUnitOfWork unitOfWork,
        IMapper mapper,
        ITaskRepository repoTask,
        IOptions<AppConfig> optionsAppConfig)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repoTask = repoTask;
            _appConfig = optionsAppConfig.Value;
        }
        public async Task<GenericResponse<Core.Entities.Task>> CreateTaskAsync(Core.Entities.Task entity)
        {
            var response = new GenericResponse<Core.Entities.Task>();

             var spec = new ProjectGetAllByFilterSpecification(new ProjectSpecParams { Id = entity.ProjectId, EnabledIncludeTasks = true });
            var entityProject = await _unitOfWork.Repository<Project>().GetEntityWithSpec(spec);

              TaskTemplateMethod projectLimit = new ProjectValidateLimit(_appConfig);
            
            if (!projectLimit.Validate(entityProject))
            {
                response.Error = new MessageResponse { Message = "Projeto com limite excedido de Tarefas associadas! "};
                return response;
            }

            await _unitOfWork.BeginTransactionAsync();
            _unitOfWork.Repository<Core.Entities.Task>().Add(entity);

            var result = await _unitOfWork.SaveChangesAsync();

            if (result.Error != null)
            {
                await _unitOfWork.RollbackAsync();

                response.Error = result.Error;
                return response;
            }

            //     var resultAuditLog = await _unitOfWork.BeforeSaveChanges();

            //  if (resultAuditLog.Error != null)
            //     {
            //         response.Error = new MessageResponse { Message = resultAuditLog.Error.Message };
            //         return response;
            //     }

            await _unitOfWork.CommitAsync();
            

            return response;
        }
        public async Task<GenericResponse<Core.Entities.Task>> UpdateTaskAsync(int id, Core.Entities.Task entityUpdate)
        {
            var response = new GenericResponse<Core.Entities.Task>();

            var spec = new TaskGetAllByFilterSpecification(new TaskSpecParams { Id = id });
            var entity = await _repoTask.GetEntityWithSpec(spec);

            if (entity != null)
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.Repository<Core.Entities.Task>().Update(entityUpdate);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result.Error != null)
                {
                    await _unitOfWork.RollbackAsync();

                    response.Error = result.Error;
                    return response;
                }

                await _unitOfWork.CommitAsync();

                return response;
            }

            response.Error = new MessageResponse();
            response.Error.Message = $"Não foi possível encontrar a Task com o {entityUpdate.TaskId}";
            return response;
        }

        public async Task<GenericResponse<bool>> ExcludeTaskAsync(int id)
        {
            var response = new GenericResponse<bool>();

            var spec = new TaskGetAllByFilterSpecification(new TaskSpecParams { Id = id });
            var entity = await _repoTask.GetEntityWithSpec(spec);

            if (entity != null)
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.Repository<Core.Entities.Task>().Delete(entity);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result.Error != null)
                {
                    await _unitOfWork.RollbackAsync();

                    response.Error = result.Error;
                    return response;
                }

                await _unitOfWork.CommitAsync();

                return response;
            }

            response.Error = new MessageResponse();
            response.Error.Message = $"Não foi possível encontrar a Task com o {id}";
            return response;
        }

    }
}
