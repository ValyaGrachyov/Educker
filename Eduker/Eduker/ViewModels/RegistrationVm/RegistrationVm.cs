using System.ComponentModel.DataAnnotations;

namespace Eduker.ViewModels.RegistrationVm
{
    public class RegistrationVm
    {
        [Required(ErrorMessage = "Need Name")]
        [MaxLength(256)]

        public string Name { get; set; }

        [Required(ErrorMessage = "Need Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required] [MaxLength(256)] public string Password { get; set; }

        [Required]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}