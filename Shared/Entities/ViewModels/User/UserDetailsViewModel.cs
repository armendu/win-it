namespace Entities.ViewModels.User
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
        
        public AddressViewModel Address { get; set; }
    }
}