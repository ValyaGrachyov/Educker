using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [PrimaryKey("Id")]
    public class UserInfo
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }

        [ForeignKey(nameof(IdentityUser))]
        public string UserId { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
    }
}