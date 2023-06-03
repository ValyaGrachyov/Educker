using System.ComponentModel.DataAnnotations;

namespace Eduker.Areas.Admin.ViewModels.UserVm
{
    public class AddUserVm
    {
        [Required(ErrorMessage = "Неверное имя")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Неверный Email ")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверная роль")]
        [MaxLength(100)]
        public string Role { get; set; }

        [Required(ErrorMessage = "Заполнить")] public string Address { get; set; }
        [Required(ErrorMessage = "Заполнить")] public string RealName { get; set; }
    }
}