using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories.Tasks;
using Core.Interfaces.Services.Tasks;
using Infra.Services;
using Microsoft.Extensions.Options;

namespace Infra.Proxy
{
    [ExcludeFromCodeCoverage]
    public class TaskServiceProxyLogging : ITaskService
    {
        private readonly ITaskService _taskService;
        private readonly IUnitOfWork _unitOfWork;

        public TaskServiceProxyLogging(
         ITaskService taskService, IUnitOfWork unitOfWork)
        {
            _taskService = taskService;
            _unitOfWork = unitOfWork;
        }


        public async Task<GenericResponse<Core.Entities.Task>> CreateTaskAsync(Core.Entities.Task entity)
        {
            var response = new GenericResponse<Core.Entities.Task>();
            Console.WriteLine($"Generate Logging ${nameof(Core.Entities.Task)}");

            await _unitOfWork.BeginTransactionAsync();

            response = await _taskService.CreateTaskAsync(entity);            

            if (response.Error != null)
            {
                await _unitOfWork.RollbackAsync();

                response.Error = response.Error;
                return response;
            }

            var resultAuditLog = await _unitOfWork.BeforeSaveChanges();

            if (resultAuditLog.Error != null)
            {
                response.Error = new MessageResponse { Message = resultAuditLog.Error.Message };
                return response;
            }
            
            await _unitOfWork.CommitAsync();
            return response;
        }

        public Task<GenericResponse<bool>> ExcludeTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<Core.Entities.Task>> UpdateTaskAsync(int id, Core.Entities.Task entity)
        {
            throw new NotImplementedException();
        }
    }
}