using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.User
{
    public class EditUserViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Birthdate { get; set; }
    }
}