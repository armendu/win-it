using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?!.*[-_]{2,})(?=^[^-_].*[^-_]$)[\w\s-]{3,15}$", ErrorMessage = "Please write a valid username between 3 and 15 characters.")]
        public string Username { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        [Required]
        [UIHint("password")]
        public string ReTypePassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}