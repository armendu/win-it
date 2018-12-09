using System;
using System.Linq;
using DataAccess.Mapping;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database
{
    public class EntityContext : DbContext
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<GameBet> Gamebets { get; set; }
        public virtual DbSet<GameInfo> Gameinfo { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameWinner> Gamewinners { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UserInfo> Userinfo { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Do the mapping here
            modelBuilder.ApplyConfiguration(new AddressesMapping());
            modelBuilder.ApplyConfiguration(new CitiesMapping());
            modelBuilder.ApplyConfiguration(new CountriesMapping());
            modelBuilder.ApplyConfiguration(new GameBetsMapping());
            modelBuilder.ApplyConfiguration(new GameInfoMapping());
            modelBuilder.ApplyConfiguration(new GamesMapping());
            modelBuilder.ApplyConfiguration(new GameWinnersMapping());
            modelBuilder.ApplyConfiguration(new PlayersMapping());
            modelBuilder.ApplyConfiguration(new RolesMapping());
            modelBuilder.ApplyConfiguration(new TransactionsMapping());
            modelBuilder.ApplyConfiguration(new UserInfoMapping());
            modelBuilder.ApplyConfiguration(new UsersMapping());
            
            //            // shadow properties
            //            modelBuilder.Entity<UserInfo>().Property<DateTime>("UpdatedAt");
            //            modelBuilder.Entity<Rule>().Property<DateTime>("UpdatedAt");

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

//            UpdateUpdatedProperty<UserInfo>();
//            UpdateUpdatedProperty<Role>();
//            DefineCreatedTimeForProperty<UserInfo>();
//            DefineCreatedTimeForProperty<Role>();
            
            return base.SaveChanges();
        }

        private void UpdateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }

        private void DefineCreatedTimeForProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}