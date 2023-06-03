using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eduker.Areas.Admin.ViewModels.RolesVm
{
    public class AddRoleVm
    {
        [Required(ErrorMessage = "Неверное название роли или она существует")]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}