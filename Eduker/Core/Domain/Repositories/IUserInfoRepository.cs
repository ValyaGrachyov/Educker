using Contract.ClientDto.UserDto;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserInfoRepository
    {
        public Task<UserInfo> GetUserById(string id);
        public Task<bool> EditUserInfo(UserInfo userInfo);
        public Task AddUserInfo(UserInfo userInfo);

        public Task DeleteUserInfo(string id);
    }
}