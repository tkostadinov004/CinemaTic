using CinemaTic.Core.Contracts;
using CinemaTic.Core.Profiles;
using CinemaTic.Core.Services;
using CinemaTic.Data;
using CinemaTic.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["CinemaTic:ConnectionString"];
builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddRoles<IdentityRole>()
   .AddEntityFrameworkStores<CinemaDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<CinemasProfile>();
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOwnersService, OwnersService>();
builder.Services.AddScoped<ISectorsService, SectorsService>();
builder.Services.AddScoped<IChartsService, ChartsService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else if (app.Environment.IsProduction())
{
    app.UseStatusCodePagesWithReExecute("/statuscode={0}");
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using var scope = app.Services.CreateScope();
var initializer = new DbInitializer(scope.ServiceProvider.GetService<CinemaDbContext>(), scope.ServiceProvider.GetService<UserManager<ApplicationUser>>());
initializer.Run(scope.ServiceProvider, true);

app.Run();