using AutoMapper;
using Core.DTOs.Autors;
using Core.Entities;

namespace Core.AutoMapper.TaskComments
{
    public class TaskCommentProfile : Profile
    {
        public TaskCommentProfile()
        {
            CreateMap<TaskComment, TaskCommentReturnDto>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.TaskCommentId))
                .ForMember(x => x.TaskId, x => x.MapFrom(d => d.TaskId))
                .ForMember(x => x.Description, x => x.MapFrom(d => d.TaskCommentDescription))
                .ReverseMap();
            
            CreateMap<TaskComment, TaskCommentFullReturnDto>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.TaskCommentId))
                .ForMember(x => x.TaskId, x => x.MapFrom(d => d.TaskId))
                .ForMember(x => x.Description, x => x.MapFrom(d => d.TaskCommentDescription))
                .ForMember(x => x.Task, x => x.MapFrom(d => d.Task))
                .ReverseMap();

            CreateMap<TaskCommentCreateDto, TaskComment>()
                .ForMember(x => x.TaskId, x => x.MapFrom(d => d.TaskId))
                .ForMember(x => x.TaskCommentDescription, x => x.MapFrom(d => d.Description))
                .ReverseMap();

            CreateMap<TaskCommentUpdateDto, TaskComment>()
                .ForMember(x => x.TaskId, x => x.MapFrom(d => d.TaskId))
                .ForMember(x => x.TaskCommentDescription, x => x.MapFrom(d => d.Description))
                .ReverseMap();
        }
    }
}