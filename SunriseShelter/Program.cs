using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sunrise_Shelter.Data;
using SunriseShelter.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SunriseShelterDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SunriseShelterDbContextConnection' not found.");

builder.Services.AddDbContext<SunriseShelterDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SunriseShelterUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SunriseShelterDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

DatabaseStartup.StartUp(app);   // This will run the dummy data in the database //

app.Run();
