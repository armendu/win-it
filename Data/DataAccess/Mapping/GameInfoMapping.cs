using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class GameInfoMapping : IEntityTypeConfiguration<GameInfo>
    {
        public void Configure(EntityTypeBuilder<GameInfo> entity)
        {
            entity.ToTable("gameinfo");

            entity.Property(e => e.GameInfoId).HasColumnType("int(11)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.Property(e => e.WinningNumbers).HasColumnType("varchar(50)");

            entity.Property(e => e.WinningPot).HasColumnType("decimal(10,0)");
        }
    }
}