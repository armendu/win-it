using System;
using System.IO;
using System.Linq;
using Common.Helpers;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Database
{
    public class EntityContext: DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
//            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
//            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);
//
//            // shadow properties
//            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
//            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

//            UpdateUpdatedProperty<SourceInfo>();
//            UpdateUpdatedProperty<DataEventRecord>();

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
    }
}