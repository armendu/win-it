using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("ROLES");

            entity.HasKey(t => t.RoleID);

            entity.Property(t => t.Name)
                .HasColumnType("varchar(100)")
                .IsRequired(true);

            entity.Property(t => t.Description)
                .HasMaxLength(250)
                .IsRequired(false);
        }
    }
}