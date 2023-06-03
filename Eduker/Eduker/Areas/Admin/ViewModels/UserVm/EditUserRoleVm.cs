using System.ComponentModel.DataAnnotations;


namespace Eduker.Areas.Admin.ViewModels.UserVm
{
    public class EditUserRoleVm
    {
        [Required(ErrorMessage = "Неверная роль")]
        public string Name { get; set; }
    }
}