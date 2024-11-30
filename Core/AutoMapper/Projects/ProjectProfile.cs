using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Core.DTOs.Projects;
using Core.Entities;

namespace Core.AutoMapper.Projects
{
    [ExcludeFromCodeCoverage]
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReturnDto>()
                .ForMember(x => x.Id, o => o.MapFrom(d => d.ProjectId))
                .ForMember(x => x.CreatedAt, o => o.MapFrom(d => d.CreatedAt))
                .ForMember(x => x.Name, o => o.MapFrom(d => d.ProjectName))
                .ForMember(x => x.Status, o => o.MapFrom(d => d.ProjectStatus))
                .ForMember(x => x.UserId, o => o.MapFrom(d => d.ProjectUserId))
                .ForMember(x => x.CompletedAt, o => o.MapFrom(d => d.ProjectCompletedAt))
                .ForMember(x => x.MaxLimitTask, o => o.MapFrom(d => d.ProjectMaxLimitTask))
                .ReverseMap();

            CreateMap<Project, ProjectFullReturnDto>()
                .ForMember(x => x.Id, o => o.MapFrom(d => d.ProjectId))
                .ForMember(x => x.CreatedAt, o => o.MapFrom(d => d.CreatedAt))
                .ForMember(x => x.Name, o => o.MapFrom(d => d.ProjectName))
                .ForMember(x => x.Status, o => o.MapFrom(d => d.ProjectStatus))
                .ForMember(x => x.UserId, o => o.MapFrom(d => d.ProjectUserId))
                .ForMember(x => x.CompletedAt, o => o.MapFrom(d => d.ProjectCompletedAt))
                .ForMember(x => x.MaxLimitTask, o => o.MapFrom(d => d.ProjectMaxLimitTask))
                .ForMember(x => x.TaskList, o => o.MapFrom(d => d.TaskList))
                .ReverseMap();                

            CreateMap<ProjectCreateDto, Project>()
                .ForMember(x => x.ProjectName, o => o.MapFrom(d => d.Name))
                .ForMember(x => x.ProjectStatus, o => o.MapFrom(d => d.Status))
                .ForMember(x => x.ProjectUserId, o => o.MapFrom(d => d.UserId))
                .ReverseMap();

            CreateMap<ProjectUpdateDto, Project>()
                .ForMember(x => x.ProjectId, o => o.MapFrom(d => d.Id))
                .ForMember(x => x.ProjectUserId, o => o.MapFrom(d => d.UserId))
                .ForMember(x => x.ProjectName, o => o.MapFrom(d => d.Name))
                .ForMember(x => x.ProjectStatus, o => o.MapFrom(d => d.Status))
                .ForMember(x => x.ProjectUserId, o => o.MapFrom(d => d.UserId))
                .ReverseMap();

             CreateMap<Project, Project>()
             .ForMember(x => x.CreatedAt, d => d.UseDestinationValue())
             .ForMember(x => x.IsDeleted, d => d.UseDestinationValue())
             .ForMember(x => x.ProjectCompletedAt, d => d.UseDestinationValue())
                .ReverseMap();                
        }
    }
}