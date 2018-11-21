using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("ADDRESSES");

            entity.HasKey(t => t.AddressID);

            entity.Property(t => t.Street)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(t => t.ZipCode)
                .HasColumnType("varchar(10)")
                .IsRequired(false);

            entity.HasOne(t => t.City)
                .WithMany()
                .HasForeignKey(t => t.CityID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Country)
                .WithMany()
                .HasForeignKey(t => t.CountryID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}