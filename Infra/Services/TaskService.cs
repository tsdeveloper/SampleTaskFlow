using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories.TaskComments;
using Core.Interfaces.Repositories.Tasks;
using Core.Interfaces.Services.TaskComments;
using Core.Interfaces.Services.Tasks;
using Core.Specification.Tasks;
using Core.Specification.Tasks.SpecParams;

namespace Infra.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _repoTask;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper, ITaskRepository repoTask)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repoTask=repoTask;
        }
        public async Task<GenericResponse<Core.Entities.Task>> CreateTaskAsync(Core.Entities.Task entity)
        {
            var response = new GenericResponse<Core.Entities.Task>();

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Repository<Core.Entities.Task>().Add(entity);

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
        public async Task<GenericResponse<Core.Entities.Task>> UpdateTaskAsync(int id, Core.Entities.Task entityUpdate)
        {
             var response = new GenericResponse<Core.Entities.Task>();

            var spec = new TaskGetAllByFilterSpecification(new TaskSpecParams { Id = id });
            var entity = await _repoTask.GetEntityWithSpec(spec);

            if (entity !=null)
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

            if (entity !=null)
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
