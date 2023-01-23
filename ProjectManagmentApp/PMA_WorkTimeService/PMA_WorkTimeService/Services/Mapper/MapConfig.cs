using AutoMapper;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;

namespace PMA_WorkTimeService.Services.Mapper
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<UserWorkTime, UserWorkTimeDTO>().ReverseMap();
        }
    }
}
