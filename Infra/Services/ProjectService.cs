using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services.Projects;
using Core.Specification.Projects;
using Core.Specification.Projects.SpecParams;

namespace Infra.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenericResponse<Project>> CreateProjectAsync(Project entity)
        {
            var response = new GenericResponse<Project>();

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Repository<Project>().Add(entity);

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
        public async Task<GenericResponse<Project>> UpdateProjectAsync(int id, Project entity)
        {
             var response = new GenericResponse<Project>();

            var spec = new ProjectGetAllByFilterSpecification(new ProjectSpecParams { Id = id });
            var ProjectExist = await _unitOfWork.Repository<Project>().GetExistEntityWithSpec(spec);

            if (ProjectExist)
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.Repository<Project>().Update(entity);
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
            response.Error.Message = $"Não foi possível encontrar o Projeto com {id}";
            return response;
        }

        public async Task<GenericResponse<bool>> ExcludeProjectAsync(int id)
        {
            var response = new GenericResponse<bool>();

            var spec = new ProjectGetAllByFilterSpecification(new ProjectSpecParams { Id = id });
            var entity = await _unitOfWork.Repository<Project>().GetEntityWithSpec(spec);

            if (entity != null)
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.Repository<Project>().Delete(entity);
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
            response.Error.Message = $"Não foi possível encontrar o Projeto com {id}";
            return response;
        }

    }
}
