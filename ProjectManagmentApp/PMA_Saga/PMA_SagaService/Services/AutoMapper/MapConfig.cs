using AutoMapper;
using PMA_SagaService.Models;

namespace PMA_SagaService.Services.AutoMapper
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<UserTaskViewModel,UserTaskViewModelIn>().ReverseMap();
            CreateMap<CommentViewModel, CommentViewModelIn>().ReverseMap();
        }
    }
}
