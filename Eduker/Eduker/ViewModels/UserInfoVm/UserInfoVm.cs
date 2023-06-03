using System.ComponentModel.DataAnnotations;

namespace Eduker.ViewModels.UserInfoVm
{
    public class UserInfoVm
    {
        public int Id { get; set; }

        public string IdentityId { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "неверно")] public string RealName { get; set; }

        [Required(ErrorMessage = "неверно")] public string Address { get; set; }
        [Required(ErrorMessage = "неверно")] public string Phone { get; set; }
        [Required(ErrorMessage = "неверно")] public string Email { get; set; }
    }
}