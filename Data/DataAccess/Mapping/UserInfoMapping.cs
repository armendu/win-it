using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class UserInfoMapping : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> entity)
        {
            entity.ToTable("USER_INFO");

            entity.HasKey(t => t.UserInfoID);

            entity.Property(t => t.FirstName)
                .HasColumnType("varchar(60)")
                .IsRequired(true);

            entity.Property(t => t.LastName)
                .HasColumnType("varchar(60)")
                .IsRequired(true);

            entity.Property(t => t.Phone)
                .HasColumnType("varchar(20)")
                .IsRequired(false);

            entity.Property(t => t.Email)
                .HasColumnType("varchar(150)")
                .IsRequired(false);

            entity.Property(t => t.Birthdate)
                .HasColumnType("date")
                .IsRequired(true);

            entity.Property(t => t.UpdatedTimestamp)
                .HasColumnType("datetime")
                .IsRequired(true);

            entity.HasOne(t => t.User)
                .WithOne(t => t.Profile)
                .HasForeignKey<UserInfo>(t => t.UserID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}