using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class GamesMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasKey(e => e.GameId);

            entity.ToTable("games");

            entity.HasIndex(e => e.GameInfoId)
                .HasName("GameInfoId");

            entity.Property(e => e.GameId).HasColumnType("int(11)");

            entity.Property(e => e.EndTime).HasColumnType("datetime");

            entity.Property(e => e.GameInfoId).HasColumnType("int(11)");

            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.Property(e => e.GameProcessed).HasColumnType("boolean").HasDefaultValue(false);

            entity.HasOne(d => d.GameInfo)
                .WithMany(p => p.Games)
                .HasForeignKey(d => d.GameInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("games_ibfk_1");
        }
    }
}