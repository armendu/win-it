using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class CountryMapping: IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("COUNTRIES");

            entity.HasKey(t => t.CountryID);

            entity.Property(t => t.Name)
                .HasColumnType("varchar(80)")
                .IsRequired(true);
        }
    }
}