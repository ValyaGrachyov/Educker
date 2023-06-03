using Microsoft.Build.Framework;

namespace Eduker.ViewModels.UserInfoVm
{
    public class EditPasswordVm
    {
        [Required] public string ConfirmPassword { get; set; }
        [Required] public string OldPassword { get; set; }
        [Required] public string NewPassword { get; set; }
    }
}