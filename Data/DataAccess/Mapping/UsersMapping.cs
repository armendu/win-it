using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class UsersMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasOne(d => d.Player)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_ibfk_3");

//            entity.HasOne(d => d.Role)
//                .WithMany(p => p.Users)
//                .HasForeignKey(d => d.RoleId)
//                .OnDelete(DeleteBehavior.Cascade)
//                .HasConstraintName("users_ibfk_1");

            entity.HasOne(d => d.UserInfo)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.UserInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_ibfk_2");
        }
    }
}