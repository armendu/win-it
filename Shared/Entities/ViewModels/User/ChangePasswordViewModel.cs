using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ReTypePassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}