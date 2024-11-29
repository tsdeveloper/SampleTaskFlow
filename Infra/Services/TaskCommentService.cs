using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services.TaskComments;
using Core.Specification.TaskComments;
using Core.Specification.TaskComments.SpecParams;

namespace Infra.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskCommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenericResponse<TaskComment>> CreateTaskCommentAsync(TaskComment entity)
        {
            var response = new GenericResponse<TaskComment>();

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Repository<TaskComment>().Add(entity);

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
        public async Task<GenericResponse<TaskComment>> UpdateTaskCommentAsync(int id, TaskComment entity)
        {
             var response = new GenericResponse<TaskComment>();

            var spec = new TaskCommentGetAllByFilterSpecification(new TaskCommentSpecParams { Id = id });
            var TaskCommentExist = await _unitOfWork.Repository<TaskComment>().GetExistEntityWithSpec(spec);

            if (TaskCommentExist)
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.Repository<TaskComment>().Update(entity);
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
            response.Error.Message = $"Não foi possível encontrar o Comentário com o  {entity.TaskCommentId}";
            return response;
        }

        public async Task<GenericResponse<bool>> ExcludeTaskCommentAsync(int id)
        {
            var response = new GenericResponse<bool>();

            var spec = new TaskCommentGetAllByFilterSpecification(new TaskCommentSpecParams { Id = id });
            var entity = await _unitOfWork.Repository<TaskComment>().GetEntityWithSpec(spec);

            if (entity !=null)
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.Repository<TaskComment>().Delete(entity);
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
            response.Error.Message = $"Não foi possível encontrar o Comentário com o  {entity.TaskCommentId}";
            return response;
        }

        public async Task<TaskComment> GetTaskCommentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
