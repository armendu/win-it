using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("USERS");

            entity.HasKey(t => t.UserID);

            entity.Property(t => t.UserID)
                .HasMaxLength(120)
                .IsRequired(true);

            entity.Property(t => t.Email)
                .HasColumnType("varchar(150)")
                .IsRequired(true);

            entity.Property(t => t.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired(true);

            entity.Property(t => t.IsActive)
                .IsRequired(true);

            entity.HasOne(t => t.Role)
                .WithMany()
                .HasForeignKey(t => t.RoleID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}