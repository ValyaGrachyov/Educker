using System.ComponentModel.DataAnnotations;

namespace Eduker.ViewModels.LoginVm
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Пароль или логин?")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пароль или логин?")]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}