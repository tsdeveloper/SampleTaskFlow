using AutoMapper;
using Core.DTOs.Tasks;

namespace Core.AutoMapper.Tasks
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Entities.Task, TaskReturnDto>()
                   .ForMember(x => x.Id, o => o.MapFrom(d => d.TaskId))
                   .ForMember(x => x.Name, o => o.MapFrom(d => d.TaskName))
                   .ForMember(x => x.Description, o => o.MapFrom(d => d.TaskDescription))
                   .ForMember(x => x.ProjectId, o => o.MapFrom(d => d.ProjectId))
                   .ForMember(x => x.Priority, o => o.MapFrom(d => d.TaskPriority))
                   .ForMember(x => x.Status, o => o.MapFrom(d => d.TaskStatus))
                ;

            CreateMap<Entities.Task, TaskFullReturnDto>()
                   .ForMember(x => x.Id, o => o.MapFrom(d => d.TaskId))
                   .ForMember(x => x.Name, o => o.MapFrom(d => d.TaskName))
                   .ForMember(x => x.Description, o => o.MapFrom(d => d.TaskDescription))
                   .ForMember(x => x.ProjectId, o => o.MapFrom(d => d.ProjectId))
                   .ForMember(x => x.Priority, o => o.MapFrom(d => d.TaskPriority))
                   .ForMember(x => x.Status, o => o.MapFrom(d => d.TaskStatus))
                   .ForMember(x => x.Project, o => o.MapFrom(d => d.Project))
                   .ForMember(x => x.TaskCommentList, o => o.MapFrom(d => d.TaskCommentList))
                ;

            CreateMap<TaskCreateDto, Entities.Task>()
                .ForMember(x => x.TaskName, o => o.MapFrom(d => d.Name))
                .ForMember(x => x.TaskDescription, o => o.MapFrom(d => d.Description))
                .ForMember(x => x.ProjectId, o => o.MapFrom(d => d.ProjectId))
                .ForMember(x => x.TaskPriority, o => o.MapFrom(d => d.Priority))
                .ForMember(x => x.TaskStatus, o => o.MapFrom(d => d.Status))
                ;

            CreateMap<TaskUpdateDto, Entities.Task>()
                .ForMember(x => x.TaskName, o => o.MapFrom(d => d.Name))
                .ForMember(x => x.TaskDescription, o => o.MapFrom(d => d.Description))
                .ForMember(x => x.ProjectId, o => o.MapFrom(d => d.ProjectId))
                .ForMember(x => x.TaskPriority, o => o.MapFrom(d => d.Priority))
                .ForMember(x => x.TaskStatus, o => o.MapFrom(d => d.Status))
                ;

        }
    }
}