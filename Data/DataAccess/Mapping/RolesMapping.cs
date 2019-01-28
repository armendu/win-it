using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class RolesMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
//            entity.HasKey(e => e.RoleId);
//
//            entity.ToTable("roles");
//
//            entity.Property(e => e.RoleId).HasColumnType("int(11)");
//
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//
            entity.Property(e => e.Description).HasColumnType("varchar(100)");
//
//            entity.Property(e => e.Name).HasColumnType("varchar(50)");
//
//            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        }
    }
}