using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Data;
using SunriseShelter.Areas.Identity.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SunriseShelterDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SunriseShelterDbContextConnection' not found.");

builder.Services.AddDbContext<SunriseShelterDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SunriseShelterUser>(options => options.SignIn.RequireConfirmedAccount = false)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<SunriseShelterDbContext>();

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


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Parent" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<SunriseShelterUser>>();

    // Define the admin user credentials
    string adminEmail = "admin@gmail.com";
    string adminPassword = "Qwe123!";

    // Check if an admin user with this email already exists
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        // Create a new admin user with required details
        var user = new SunriseShelterUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "Admin",
            DateOfBirth = new DateTime(2008, 3, 4),  // Example date of birth
            PhoneNumber = "02102786388",
            MaritalStatus = "Single", 
            Address = "123 Admin Street", 
            BirthPlace = "New Zealand", 
            EmailConfirmed = true // Confirm email so user can log in immediately
        };

        // Create the admin user in the database with the given password
        await userManager.CreateAsync(user, adminPassword);

        // Assign the "Admin" role to this user
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

// Seed the database with dummy data
await DatabaseStartup.StartUp(app);   

// Run the application
app.Run();