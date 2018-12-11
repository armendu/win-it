using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class GameWinnersMapping : IEntityTypeConfiguration<GameWinner>
    {
        public void Configure(EntityTypeBuilder<GameWinner> entity)
        {
            entity.HasKey(e => new {e.GameId, e.PlayerId});

            entity.ToTable("gamewinners");

            entity.HasIndex(e => e.PlayerId)
                .HasName("PlayerId");

            entity.Property(e => e.GameId).HasColumnType("int(11)");

            entity.Property(e => e.PlayerId).HasColumnType("int(11)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.NumbersMatches).HasColumnType("int(11)");

            entity.Property(e => e.SumWon).HasColumnType("decimal(10,0)");

            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Game)
                .WithMany(p => p.GameWinners)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("gamewinners_ibfk_1");

            entity.HasOne(d => d.Player)
                .WithMany(p => p.GameWinners)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("gamewinners_ibfk_2");
        }
    }
}