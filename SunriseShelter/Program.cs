using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Data;
using SunriseShelter.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SunriseShelterDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SunriseShelterDbContextConnection' not found.");

builder.Services.AddDbContext<SunriseShelterDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SunriseShelterUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
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

DatabaseStartup.StartUp(app);   // This will run the dummy data in the database //

using (var scope = app.Services.CreateScope())
{
    var SunriseShelterRoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await SunriseShelterRoleManager.RoleExistsAsync("Admin"))
        await SunriseShelterRoleManager.CreateAsync(new IdentityRole("Admin"));
}

using (var scope = app.Services.CreateScope())
{
    var SunriseShelterUserManager = scope.ServiceProvider.GetRequiredService<UserManager<SunriseShelterUser>>();

    string adminEmail = "admin@sunriseshelter.com";
    string adminPassword = "Admin";


    if (await SunriseShelterUserManager.FindByEmailAsync(adminEmail) == null)
    {
        var user = new SunriseShelterUser();
        user.UserName = adminEmail;
        user.Email = adminEmail;
        user.FirstName = "Test";
        user.LastName = "Admin";

        await SunriseShelterUserManager.CreateAsync(user, adminPassword);
        await SunriseShelterUserManager.AddToRoleAsync(user, "Admin");
    }
}


app.Run();
