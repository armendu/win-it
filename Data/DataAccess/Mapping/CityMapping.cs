using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> entity)
        {
            entity.ToTable("CITIES");

            entity.HasKey(t => t.CityID);

            entity.Property(t => t.Name)
                .HasColumnType("varchar(80)")
                .IsRequired(true);

            entity.HasOne(t => t.Country)
                .WithMany()
                .HasForeignKey(t => t.CountryID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}