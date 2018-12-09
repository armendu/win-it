using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class PlayersMapping : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> entity)
        {
            entity.HasKey(e => e.PlayerId);

            entity.ToTable("players");

            entity.Property(e => e.PlayerId).HasColumnType("int(11)");

            entity.Property(e => e.Balance).HasColumnType("decimal(10,0)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.NumberOfGamesPlayed).HasColumnType("int(11)");

            entity.Property(e => e.NumberOfGamesWon).HasColumnType("int(11)");

            entity.Property(e => e.TotalSpent).HasColumnType("decimal(10,0)");

            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        }
    }
}