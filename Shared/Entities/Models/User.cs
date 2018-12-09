namespace Entities.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int? RoleId { get; set; }
        public int? UserInfoId { get; set; }
        public int? PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
