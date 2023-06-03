using System.ComponentModel.DataAnnotations;

namespace Eduker.Areas.Admin.ViewModels.UserVm
{
    public class EditUserVm
    {
        [Required(ErrorMessage = "Неверное имя или существующее")]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Неверная почта")]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполнить")]
        [MaxLength(256)]
        public string RealName { get; set; }

        [Required(ErrorMessage = "Заполнить")] public string Address { get; set; }
    }
}