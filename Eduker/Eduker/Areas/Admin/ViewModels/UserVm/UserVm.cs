using System.ComponentModel.DataAnnotations;

namespace Eduker.Areas.Admin.ViewModels.UserVm
{
    public class UserVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}