using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Username { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        [Required]
        [UIHint("password")]
        public string ReTypePassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}