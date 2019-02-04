using System;
using System.Threading.Tasks;
using DataAccess.Mapping;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Database
{
    public class EntityContext : DbContext
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<GameBet> GameBets { get; set; }
        public virtual DbSet<GameInfo> GameInfo { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameWinner> GameWinners { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<IdentityUserRole<Guid>> UserRoles { get; set; }
        public virtual DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; }
        public virtual DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; }
        public virtual DbSet<GameSettings> GameSettings { get; set; }

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
            modelBuilder.ApplyConfiguration(new IdentityUserRoleMapping());
            modelBuilder.ApplyConfiguration(new GameSettingsMapping());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Creates a default admin user during the first build.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="configuration">The configuration to be used.</param>
        /// <returns></returns>
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<Role> roleManager =
                serviceProvider.GetRequiredService<RoleManager<Role>>();
            string username = configuration["AdminUser:UserName"];
            string email = configuration["AdminUser:Email"];
            string password = configuration["AdminUser:Password"];
            string role = configuration["AdminUser:Role"];
            string description = configuration["AdminUser:Description"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new Role(role, description));
                    await roleManager.CreateAsync(new Role("Default", "The default users of the application"));
                }

                UserInfo userInfo = new UserInfo
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Phone = string.Empty,
                    UpdateAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                };

                User user = new User
                {
                    UserName = username,
                    Email = email,
                    IsActive = true,
                    UserInfo = userInfo
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}