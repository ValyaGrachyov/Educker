using System.ComponentModel.DataAnnotations;

namespace Eduker.ViewModels.UserInfoVm
{
    public class EditUserInfoVm
    {
        [Required(ErrorMessage = "неверно")] public string RealName { get; set; }

        [Required(ErrorMessage = "неверно")] public string Address { get; set; }
        [Required(ErrorMessage = "неверно")] public string Phone { get; set; }

        [Required(ErrorMessage = "неверно")]
        [EmailAddress]
        public string Email { get; set; }
    }
}