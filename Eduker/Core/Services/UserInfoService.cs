using Contract.ClientDto.UserDto;
using Domain.Repositories;
using Services.Abstraction;
using Domain.Entities;
using Contract.AdminDto.UserInfoDto;

namespace Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserInfoService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<UserInfoDto> GetUserById(string id)
        {
            var user = await _repositoryManager.UserInfoRepository.GetUserById(id);

            if (user == null)
            {
                throw new NullReferenceException();
            }

            return new UserInfoDto
            {
                Id = user.Id,
                Address = user.Address,
                RealName = user.RealName,
                UserIdentityId = user.UserId
            };
        }

        public async Task DeleteUserInfo(string id)
        {
            await _repositoryManager.UserInfoRepository.DeleteUserInfo(id);
        }

        public async Task AddUserInfo(AddUserInfoDto userInfoDto)
        {
            var newUserInfo = new UserInfo
            {
                RealName = userInfoDto.RealName,
                Email = userInfoDto.Email,
                Address = userInfoDto.Address,
                UserId = userInfoDto.IdentityId
            };

            await _repositoryManager.UserInfoRepository.AddUserInfo(newUserInfo);
        }

        public async Task<bool> EditUserInfo(EditUserInfoDto userInfodto)
        {
            var newUserInfo = new UserInfo
            {
                Id = userInfodto.Id,
                Address = userInfodto.Address,
                Email = userInfodto.Email,
                RealName = userInfodto.RealName,
                UserId = userInfodto.UserId
            };
            var result = await _repositoryManager.UserInfoRepository.EditUserInfo(newUserInfo);

            if (result == true)
            {
                return true;
            }

            return false;
        }
    }
}