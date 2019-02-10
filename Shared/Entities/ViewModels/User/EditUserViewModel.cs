using System;

namespace Entities.ViewModels.User
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Birthdate { get; set; }
    }
}