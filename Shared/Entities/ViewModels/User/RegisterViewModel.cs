using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?!.*[-_]{2,})(?=^[^-_].*[^-_]$)[\w\s-]{3,15}$", ErrorMessage =
            "Please write a valid username between 3 and 15 characters.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [UIHint("password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ReTypePassword { get; set; }

        [Required]
        [Display(Name = "First name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public string Birthdate { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}