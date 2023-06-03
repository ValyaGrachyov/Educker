using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ClientDto.UserDto
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string RealName { get; set; }
        public string UserIdentityId { get; set; }
    }
}