using AutoMapper;
using PMA_SagaService.Models;

namespace PMA_SagaService.Services.AutoMapper
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<UserTaskViewModel,UserTaskViewModelIn>().ReverseMap();
            CreateMap<UserTaskCreateViewModel, UserTaskViewModelIn>().ReverseMap();
            CreateMap<UserTaskCreateViewModel, UserTaskResponseViewModel>()
                .ForMember(m => m.userTaskStatusId, opt => opt.MapFrom(src => src.statusId))
                .ForMember(m => m.taskName, opt => opt.MapFrom(src => src.name))
                .ReverseMap();
            CreateMap<CommentViewModel, CommentViewModelIn>().ReverseMap();
        }
    }
}
