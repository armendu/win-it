using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.User
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}