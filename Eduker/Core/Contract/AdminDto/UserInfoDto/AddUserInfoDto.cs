using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.AdminDto.UserInfoDto
{
    public class AddUserInfoDto
    {
        public string IdentityId { get; set; }
        public string RealName { get; set; } = "";
        public string Email { get; set; }
        public string Address { get; set; } = "";
    }
}