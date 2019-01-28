using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class GameSettingsMapping : IEntityTypeConfiguration<GameSettings>
    {
        public void Configure(EntityTypeBuilder<GameSettings> entity)
        {
            entity.HasKey(e => e.GameSettingId);

            entity.Property(e => e.GameLength).HasColumnType("int(11)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        }
    }
}