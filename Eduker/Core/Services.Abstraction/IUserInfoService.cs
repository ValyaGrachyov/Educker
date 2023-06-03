using Contract.AdminDto.UserInfoDto;
using Contract.ClientDto.UserDto;

namespace Services.Abstraction
{
    public interface IUserInfoService
    {
        public Task<UserInfoDto> GetUserById(string id);
        public Task<bool> EditUserInfo(EditUserInfoDto userInfodto);
        public Task AddUserInfo(AddUserInfoDto userInfoDto);
        public Task DeleteUserInfo(string id);
    }
}