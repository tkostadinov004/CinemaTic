using Cinema.Data;
using Cinema.Models;
using Cinema.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema
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
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        //private void CreateRoles(IServiceProvider serviceProvider)
        //{
        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //        Task<IdentityResult> roleResult;
        //        string email = "admin@admin.com";

        //        //Check that there is an Administrator role and create if not
        //        Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
        //        hasAdminRole.Wait();

        //        if (!hasAdminRole.Result)
        //        {
        //            roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
        //            roleResult.Wait();
        //        }

        //        Task<bool> hasOwnerRole = roleManager.RoleExistsAsync("Owner");
        //        hasOwnerRole.Wait();

        //        if (!hasOwnerRole.Result)
        //        {
        //            roleResult = roleManager.CreateAsync(new IdentityRole("Owner"));
        //            roleResult.Wait();
        //        }

        //        Task<bool> hasVisitorRole = roleManager.RoleExistsAsync("Visitor");
        //        hasVisitorRole.Wait();

        //        if (!hasVisitorRole.Result)
        //        {
        //            roleResult = roleManager.CreateAsync(new IdentityRole("Visitor"));
        //            roleResult.Wait();
        //        }
        //        //Check if the admin user exists and create it if not
        //        //Add to the Administrator role

        //        Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
        //        testUser.Wait();

        //        if (testUser.Result == null)
        //        {
        //            ApplicationUser administrator = new ApplicationUser();
        //            administrator.Email = email;
        //            administrator.UserName = email;
        //            administrator.FirstName = "Admin";
        //            administrator.LastName = "Adminovski";

        //            Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "adminPass123*");  
        //            newUser.Wait();

        //            if (newUser.Result.Succeeded)
        //            {
        //                Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
        //                newUserRole.Wait();
        //            }
        //        }
        //    }
        //}
        //public async Task CreateRolesAsync(IServiceProvider serviceProvider)
        //{
        //    var roles = new string[] { "Owner", "Root", "Visitor" };

        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole { Name = role });
        //        }
        //    }
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            //CreateRoles(app.ApplicationServices);
        }
    }
}
