using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class TransactionsMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> entity)
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("transactions");

            entity.HasIndex(e => e.GameId)
                .HasName("GameId");

            entity.HasIndex(e => e.PlayerId)
                .HasName("PlayerId");

            entity.Property(e => e.TransactionId).HasColumnType("int(11)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.GameId).HasColumnType("int(11)");

            entity.Property(e => e.PlayerId).HasColumnType("int(11)");

            entity.Property(e => e.Sum).HasColumnType("decimal(10,0)");

            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Game)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transactions_ibfk_2");

            entity.HasOne(d => d.Player)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transactions_ibfk_1");
        }
    }
}