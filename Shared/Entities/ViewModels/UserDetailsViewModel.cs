using System;

namespace Entities.ViewModels
{
    public class UserDetailsViewModel
    {
        public int ProfileID { get; set; }

        public string LoginEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public DateTime Birthdate { get; set; }

        public AddressViewModel Address { get; set; }

        public string Role { get; set; }

        public DateTime DateRegistered { get; set; }
    }
}