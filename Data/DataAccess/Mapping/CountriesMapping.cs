using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class CountriesMapping: IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.HasKey(e => e.CountryId);

            entity.ToTable("countries");

            entity.Property(e => e.CountryId).HasColumnType("int(11)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.Name).HasColumnType("varchar(50)");

            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        }
    }
}