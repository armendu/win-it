using Microsoft.AspNetCore.Identity;

namespace Entities.ViewModels.User
{
    public class RegisterResultViewModel
    {
        public IdentityResult Result { get; set; }
        public Models.User User { get; set; }
    }
}