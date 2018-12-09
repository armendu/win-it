using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class CitiesMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> entity)
        {
            entity.HasKey(e => e.CityId);

            entity.ToTable("cities");

            entity.HasIndex(e => e.CountryId)
                .HasName("CountryId");

            entity.Property(e => e.CityId).HasColumnType("int(11)");

            entity.Property(e => e.CountryId).HasColumnType("int(11)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.Name).HasColumnType("varchar(50)");

            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("cities_ibfk_1");
        }
    }
}