using AutoMapper;
using PMA_IdentityService.Models;
using PMA_IdentityService.Models.DTOs;
using PMA_IdentityService.Models.ViewModels;

namespace PMA_IdentityService.Services.Mapper
{
    public class MapConfig : Profile
    {
        public MapConfig() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, UserInfoViewModel>().ReverseMap();
            CreateMap<UserRegistrationViewModel,UserDTO>().ForMember(m => m.Position_Id, opt => opt.MapFrom(src => src.PositionId)).ReverseMap();
            CreateMap<Position, PositionDTO>().ReverseMap();
        }
    }
}
