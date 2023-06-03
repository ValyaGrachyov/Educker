using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ClientDto.UserDto
{
    public class EditUserInfoDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public string UserId { get; set; }
    }
}