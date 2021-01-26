using System;
using AutoMapper;
using AutomapperProfiles.Profiles;
using Data.DbContexts;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Constants;
using Models.Query;
using Npgsql;
using Services.Classes;
using Services.Interfaces;
using Utility;

namespace WebApp
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
            services.AddRazorPages();
            
            services.AddResponseCaching();

            services.AddResponseCompression();

            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseNpgsql(
                    new NpgsqlConnectionStringBuilder
                    {
                        Host = EnvironmentVariables.PostgresqlHost,
                        Port = int.Parse(EnvironmentVariables.PostgresqlPort),
                        Username = EnvironmentVariables.PostgresqlUsername,
                        Password = EnvironmentVariables.PostgresqlPassword,
                        Database = EnvironmentVariables.PostgresqlDatabaseName,
                        SslMode = SslMode.Require,
                        TrustServerCertificate = true
                    }.ConnectionString,
                    npgsqlDbContextOptionsBuilder =>
                    {
                        options.UseLazyLoadingProxies();
                        npgsqlDbContextOptionsBuilder.MigrationsAssembly("WebApp");
                    });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MainDbContext>()
                .AddRoles<IdentityRole>();

            services.AddAuthentication();

            services.AddAuthorization();
            
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
            });

            services.AddScoped<IBaseService<Account, AccountQueryModel>, AccountService>();
            services.AddScoped<IBaseService<Category, CategoryQueryModel>, CategoryService>();
            services.AddScoped<IBaseService<Entry, EntryQueryModel>, EntryService>();
            
            services.AddAutoMapper(typeof(AccountProfile));
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(typeof(EntryProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            BootstrapStaticClasses();

            app.UseHttpsRedirection();
            
            app.UseResponseCompression();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages().RequireAuthorization();
            });
        }

        private static void BootstrapStaticClasses()
        {
            CurrencyInfo.GenerateAll();
        }
    }
}