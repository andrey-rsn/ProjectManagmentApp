using AutoMapper;
using PMA_IdentityService.Models.ViewModels;
using PMA_IdentityService.Repositories;

namespace PMA_IdentityService.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public UserInfoService(IUserRepository userRepository, IPositionRepository positionRepository, IMapper mapper = null)
        {
            _userRepository = userRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<UserInfoViewModel> GetUserInfo(int User_Id)
        {
            var User = await _userRepository.GetById(User_Id);

            if(User != null) 
            {
                var Position = await _positionRepository.GetById(User.Position_Id);

                if(Position != null)
                {
                    var UserInfo = _mapper.Map<UserInfoViewModel>(User);

                    UserInfo.Position = Position.PositionName;

                    return UserInfo;
                }
            }

            return new UserInfoViewModel();
        }

        public async Task<IEnumerable<UserInfoViewModel>> GetUsersInfosByIds(int[] Users_Ids)
        {
            var Result = new List<UserInfoViewModel>();

            foreach (var UserId in Users_Ids)
            {
                var User = await _userRepository.GetById(UserIdd);

                if (User != null)
                {
                    var Position = await _positionRepository.GetById(User.Position_Id);

                    if (Position != null)
                    {
                        var UserInfo = _mapper.Map<UserInfoViewModel>(User);

                        UserInfo.Position = Position.PositionName;

                        Result.Add(UserInfo);
                    }
                }

            }
        }
    }
}
