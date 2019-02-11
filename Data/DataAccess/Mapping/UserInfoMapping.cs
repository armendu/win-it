using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class UserInfoMapping : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> entity)
        {
            entity.ToTable("userinfo");

            entity.HasIndex(e => e.AddressId)
                .HasName("AddressId");

            entity.Property(e => e.UserInfoId).HasColumnType("int(11)");

            entity.Property(e => e.AddressId).HasColumnType("int(11)");

            entity.Property(e => e.Birthdate).HasColumnType("date");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.FirstName).HasColumnType("varchar(50)");

            entity.Property(e => e.LastName).HasColumnType("varchar(50)");

            entity.Property(e => e.Phone).HasColumnType("varchar(25)");

            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Address)
                .WithMany(p => p.UserInfo)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userinfo_ibfk_1");
        }
    }
}