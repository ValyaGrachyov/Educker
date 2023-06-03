using System.ComponentModel.DataAnnotations;

namespace Eduker.Areas.Admin.ViewModels.RolesVm
{
    public class EditRoleVm
    {
        [Required(ErrorMessage = "Имя не подходит или существует")]
        public string Name { get; set; }
    }
}