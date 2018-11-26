using System;
using System.Linq;
using DataAccess.Mapping;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database
{
    public class EntityContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<CreditInfo> CreditInfo { get; set; }
        public DbSet<GameInfo> GameInfo { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<GameNumbers> GameNumbers { get; set; }
        public DbSet<Report> Reports { get; set; }

        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Do the mapping here
            builder.ApplyConfiguration(new AddressMapping());
            builder.ApplyConfiguration(new CityMapping());
            builder.ApplyConfiguration(new CountryMapping());
            builder.ApplyConfiguration(new RoleMapping());
            builder.ApplyConfiguration(new UserInfoMapping());
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new GamePlayerMapping());

            // shadow properties
            builder.Entity<UserInfo>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Rule>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            UpdateUpdatedProperty<UserInfo>();
            UpdateUpdatedProperty<Rule>();
            UpdateUpdatedProperty<Role>();
            DefineCreatedTimeForProperty<UserInfo>();
            DefineCreatedTimeForProperty<Rule>();
            DefineCreatedTimeForProperty<Role>();
            
            return base.SaveChanges();
        }

        private void UpdateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
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