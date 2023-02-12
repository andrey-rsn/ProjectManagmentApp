using AutoMapper;
using PMA_TasksService.Models;
using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Services.AutoMapper
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<UserTask,UserTaskDTO>().ReverseMap();
            CreateMap<UserTaskStatus,UserTaskStatusDTO>().ReverseMap();
            CreateMap<TaskComments,TaskCommentsDTO>().ReverseMap();
            CreateMap<Comment,CommentDTO>().ReverseMap();
            CreateMap<UserTaskViewModel, UserTaskDTO>()
                .ForMember(dest => dest.userTaskStatusId, o => o.MapFrom(src => src.statusId))
                .ForMember(dest => dest.taskId, o => o.MapFrom(src => src.id))
                .ForMember(dest => dest.assignedUserId, o => o.MapFrom(src => src.assignedUserId))
                .ForMember(dest => dest.taskName, o => o.MapFrom(src => src.name))
                .ForMember(dest => dest.changeDate, o => o.MapFrom(src => src.changeDate))
                .ForMember(dest => dest.changedByUserId, o => o.MapFrom(src => src.changedByUserId))
                .ForMember(dest => dest.description, o => o.MapFrom(src => src.description))
                .ForMember(dest => dest.priority, o => o.MapFrom(src => src.priority))
                .ReverseMap();
        }
    }
}
