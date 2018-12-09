using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class UsersMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("users");

            entity.HasIndex(e => e.PlayerId)
                .HasName("PlayerId");

            entity.HasIndex(e => e.RoleId)
                .HasName("RoleId");

            entity.HasIndex(e => e.UserId)
                .HasName("UserId")
                .IsUnique();

            entity.HasIndex(e => e.UserInfoId)
                .HasName("UserInfoId");

            entity.Property(e => e.UserId).HasColumnType("varchar(150)");

            entity.Property(e => e.Email).HasColumnType("varchar(100)");

            entity.Property(e => e.PlayerId).HasColumnType("int(11)");

            entity.Property(e => e.RoleId).HasColumnType("int(11)");

            entity.Property(e => e.UserInfoId).HasColumnType("int(11)");

            entity.Property(e => e.Username).HasColumnType("varchar(100)");

            entity.HasOne(d => d.Player)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_ibfk_3");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_ibfk_1");

            entity.HasOne(d => d.UserInfo)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.UserInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_ibfk_2");
        }
    }
}