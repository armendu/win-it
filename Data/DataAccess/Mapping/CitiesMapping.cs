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
            
            entity.Property(e => e.CityId).HasColumnType("int(11)");

            entity.Property(e => e.Name).HasColumnType("varchar(100)");

            entity.Property(e => e.Country).HasColumnType("varchar(100)");
        }
    }
}