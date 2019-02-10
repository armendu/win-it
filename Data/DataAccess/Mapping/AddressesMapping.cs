using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class AddressesMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
                entity.HasKey(e => e.AddressId);

                entity.ToTable("addresses");

                entity.HasIndex(e => e.CityId)
                    .HasName("CityId");

                entity.Property(e => e.AddressId).HasColumnType("int(11)");

                entity.Property(e => e.CityId).HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Street).HasColumnType("varchar(100)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.ZipCode).HasColumnType("varchar(15)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("addresses_ibfk_1");
        }
    }
}