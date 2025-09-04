using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;

public class SunriseShelterDbContext : IdentityDbContext<SunriseShelterUser>
{
    public SunriseShelterDbContext(DbContextOptions<SunriseShelterDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Add your relationship configurations here
        builder.Entity<Adoption>()
            .HasOne(a => a.Parent)
            .WithMany(u => u.Adoptions)
            .HasForeignKey(a => a.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    // KEEP these DbSets:
    public DbSet<SunriseShelter.Models.Adoption> Adoption { get; set; } = default!;
    public DbSet<SunriseShelter.Models.Children> Children { get; set; } = default!;
    public DbSet<SunriseShelter.Models.Orphanage> Orphanage { get; set; } = default!;
    public DbSet<SunriseShelter.Models.Staff> Staff { get; set; } = default!;

}