using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User: IdentityUser
    {
        public int? UserInfoId { get; set; }
        public int? PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
