using CoffeShop.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace CoffeShop
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                sql => sql.EnableRetryOnFailure()
            ));
            services.AddIdentity<IdentityUser,IdentityRole>()
                .AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddHttpContextAccessor();
            services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(10);
                Options.Cookie.HttpOnly = true;
                Options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // HSTS/HTTPS redirection are optionally controlled below via UseHttpsRedirection.
            }

            // Allow Docker (and other environments) to disable HTTPS redirection.
            var useHttpsRedirection = Configuration.GetValue<bool?>("UseHttpsRedirection") ?? true;
            if (!env.IsDevelopment() && useHttpsRedirection)
            {
                app.UseHsts();
            }
            if (useHttpsRedirection)
            {
                app.UseHttpsRedirection();
            }

            // Optionally run EF Core migrations on container startup.
            var runMigrationsOnStartup = Configuration.GetValue<bool?>("RunMigrationsOnStartup") ?? false;
            if (runMigrationsOnStartup)
            {
                using var scope = app.ApplicationServices.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var maxAttempts = Configuration.GetValue<int?>("Migrations:MaxAttempts") ?? 20;
                var initialDelayMs = Configuration.GetValue<int?>("Migrations:InitialDelayMs") ?? 1000;
                var maxDelayMs = Configuration.GetValue<int?>("Migrations:MaxDelayMs") ?? 8000;

                for (var attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    try
                    {
                        db.Database.Migrate();
                        break;
                    }
                    catch (Exception ex) when (attempt < maxAttempts && (ex is SqlException || ex.InnerException is SqlException))
                    {
                        var delayMs = Math.Min(maxDelayMs, initialDelayMs * (int)Math.Pow(2, attempt - 1));
                        logger.LogWarning(ex, "Database not ready (attempt {Attempt}/{MaxAttempts}); retrying in {DelayMs}ms.", attempt, maxAttempts, delayMs);
                        Thread.Sleep(delayMs);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to apply database migrations on startup.");
                        throw;
                    }
                }
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
