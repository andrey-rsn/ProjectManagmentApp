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
        }
    }
}
