using Contract.ClientDto.UserDto;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly EduckerDbContext _dbContext;

        public UserInfoRepository(EduckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserInfo> GetUserById(string id)
        {
            var user = await _dbContext.UsersInfo.ToListAsync();

            if (user == null)
            {
                throw new NullReferenceException("Пу-пу");
            }

            var a = user.Select(x => x).Where(x => x.UserId == id).ToList();

            return a.First();
        }

        public async Task DeleteUserInfo(string id)
        {
            var user = await GetUserById(id);
            _dbContext.UsersInfo.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserInfo(UserInfo userInfo)
        {
            await _dbContext.UsersInfo.AddAsync(userInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EditUserInfo(UserInfo userInfo)
        {
            UserInfo newUserInfo = await _dbContext.UsersInfo.FindAsync(userInfo.Id);
            newUserInfo = Edit(newUserInfo, userInfo);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private UserInfo Edit(UserInfo oldUserInfo, UserInfo newUserInfo)
        {
            oldUserInfo.RealName = newUserInfo.RealName;
            oldUserInfo.Address = newUserInfo.Address;
            oldUserInfo.Email = newUserInfo.Email;
            return oldUserInfo;
        }
    }
}