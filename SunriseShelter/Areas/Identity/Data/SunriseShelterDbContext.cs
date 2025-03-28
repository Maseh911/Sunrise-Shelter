using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;

namespace SunriseShelter.Areas.Identity.Data;

public class SunriseShelterDbContext : IdentityDbContext<SunriseShelterUser>
{
    public SunriseShelterDbContext(DbContextOptions<SunriseShelterDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<SunriseShelter.Models.Adoption> Adoption { get; set; } = default!;

public DbSet<SunriseShelter.Models.Children> Children { get; set; } = default!;

public DbSet<SunriseShelter.Models.Orphanage> Orphanage { get; set; } = default!;

public DbSet<SunriseShelter.Models.Parent> Parent { get; set; } = default!;

public DbSet<SunriseShelter.Models.Staff> Staff { get; set; } = default!;
}
