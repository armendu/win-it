using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class GameBetsMapping : IEntityTypeConfiguration<GameBet>
    {
        public void Configure(EntityTypeBuilder<GameBet> entity)
        {
            entity.HasKey(e => new { e.TransactionId, e.GameId, e.PlayerId });

            entity.ToTable("gamebets");

            entity.HasIndex(e => e.GameId)
                .HasName("GameId");

            entity.HasIndex(e => e.PlayerId)
                .HasName("PlayerId");

            entity.Property(e => e.TransactionId).HasColumnType("int(11)");

            entity.Property(e => e.GameId).HasColumnType("int(11)");

            entity.Property(e => e.PlayerId).HasColumnType("int(11)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Game)
                .WithMany(p => p.GameBets)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("gamebets_ibfk_2");

            entity.HasOne(d => d.Player)
                .WithMany(p => p.GameBets)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("gamebets_ibfk_3");

            entity.HasOne(d => d.Transaction)
                .WithMany(p => p.GameBets)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("gamebets_ibfk_1");
        }
    }
}