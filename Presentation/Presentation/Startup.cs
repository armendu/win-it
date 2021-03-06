﻿using BusinessLogic;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Common.Services;
using DataAccess.Database;
using DataAccess.Repository;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EntityContext>(options => options.UseMySql(
                Configuration.GetConnectionString("WinItConnectionString")).UseLazyLoadingProxies());

            // UserIdentity configuration
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<EntityContext>()
            .AddDefaultTokenProviders();

            // Do the dependency injections here
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleLogic, RoleLogic>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IGameLogic, GameLogic>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ITransactionLogic, TransactionLogic>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IGameSettingsLogic, GameSettingsLogic>();
            services.AddScoped<IGameSettingsRepository, GameSettingsRepository>();
            services.AddScoped<ICitiesLogic, CitiesLogic>();
            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<IPlayerLogic, PlayerLogic>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGameBetsLogic, GameBetsLogic>();
            services.AddScoped<IGameBetsRepository, GameBetsRepository>();
            services.AddHostedService<GameCreationService>();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddSession();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/User/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            // Enable Authentication
            app.UseAuthentication();

            // Create default admin account and make sure to seed the right data.
            EntityContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
            SeedData.EnsurePopulated(app, Configuration);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}