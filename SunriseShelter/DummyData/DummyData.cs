using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SunriseShelter.Data
{
    public class DatabaseStartup
    {
        public static async Task StartUp(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SunriseShelterDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<SunriseShelterUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var logger = serviceScope.ServiceProvider.GetService<ILogger<DatabaseStartup>>();

                logger.LogInformation("DatabaseStartup: Starting database seeding...");

                await context.Database.EnsureCreatedAsync();

                // Check if we already have data
                if (context.Users.Any() && context.Orphanage.Any() && context.Staff.Any() && context.Children.Any() && context.Adoption.Any())
                {
                    logger.LogInformation("DatabaseStartup: Data already exists. Skipping seed.");
                    return;
                }

                logger.LogInformation("DatabaseStartup: Seeding data...");

                // Create parent users
                var parents = new SunriseShelterUser[]
                {
                    new SunriseShelterUser { UserName = "jack.taylor@example.com", Email = "jack.taylor@example.com", FirstName = "Jack", LastName = "Taylor", DateOfBirth = new DateTime(1988, 9, 10), PhoneNumber = "0210123456", MartialStatus = "Married", Address = "Mt Eden", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "olivia.king@example.com", Email = "olivia.king@example.com", FirstName = "Olivia", LastName = "King", DateOfBirth = new DateTime(1993, 11, 2), PhoneNumber = "0210234567", MartialStatus = "Single", Address = "Onehunga", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "ethan.scott@example.com", Email = "ethan.scott@example.com", FirstName = "Ethan", LastName = "Scott", DateOfBirth = new DateTime(1985, 1, 18), PhoneNumber = "0210345678", MartialStatus = "Divorced", Address = "Mt Roskill", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lily.green@example.com", Email = "lily.green@example.com", FirstName = "Lily", LastName = "Green", DateOfBirth = new DateTime(1979, 4, 25), PhoneNumber = "0210456789", MartialStatus = "Married", Address = "Remuera", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "noah.walker@example.com", Email = "noah.walker@example.com", FirstName = "Noah", LastName = "Walker", DateOfBirth = new DateTime(1990, 6, 12), PhoneNumber = "0210567890", MartialStatus = "Single", Address = "Birkenhead", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "mia.hall@example.com", Email = "mia.hall@example.com", FirstName = "Mia", LastName = "Hall", DateOfBirth = new DateTime(1984, 8, 5), PhoneNumber = "0210678901", MartialStatus = "Widowed", Address = "Mt Wellington", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lucas.allen@example.com", Email = "lucas.allen@example.com", FirstName = "Lucas", LastName = "Allen", DateOfBirth = new DateTime(1991, 3, 14), PhoneNumber = "0210789012", MartialStatus = "Married", Address = "New Lynn", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "amelia.young@example.com", Email = "amelia.young@example.com", FirstName = "Amelia", LastName = "Young", DateOfBirth = new DateTime(1987, 5, 30), PhoneNumber = "0210890123", MartialStatus = "Single", Address = "Epsom", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "mason.harris@example.com", Email = "mason.harris@example.com", FirstName = "Mason", LastName = "Harris", DateOfBirth = new DateTime(1982, 7, 21), PhoneNumber = "0210901234", MartialStatus = "Married", Address = "Mt Albert", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "ella.martin@example.com", Email = "ella.martin@example.com", FirstName = "Ella", LastName = "Martin", DateOfBirth = new DateTime(1995, 9, 11), PhoneNumber = "0211012345", MartialStatus = "Single", Address = "Manukau", BirthPlace = "New Zealand", EmailConfirmed = true }
                };

                // Create parent users with a default password
                foreach (var parent in parents)
                {
                    var result = await userManager.CreateAsync(parent, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(parent, "Parent");
                    }
                    else
                    {
                        logger.LogError("Failed to create user {Email}: {Errors}",
                            parent.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }

                // No need to call SaveChanges here - userManager.CreateAsync already saves

                var orphanages = new Orphanage[]
                {
                    new Orphanage { Name = "Napier Haven", Address = "29 Marine Parade", State = "Hawke's Bay", Country = "New Zealand" },
                    new Orphanage { Name = "Rotorua Refuge", Address = "54 Fenton St", State = "Bay of Plenty", Country = "New Zealand" },
                    new Orphanage { Name = "New Plymouth Home", Address = "12 Devon St East", State = "Taranaki", Country = "New Zealand" },
                    new Orphanage { Name = "Nelson Shelter", Address = "33 Hardy St", State = "Nelson", Country = "New Zealand" },
                    new Orphanage { Name = "Wellington Haven", Address = "80 Cuba St", State = "Wellington", Country = "New Zealand" },
                    new Orphanage { Name = "Auckland North Home", Address = "17 Dominion Rd", State = "Auckland", Country = "New Zealand" },
                    new Orphanage { Name = "Hamilton Shelter", Address = "49 Victoria St", State = "Waikato", Country = "New Zealand" },
                    new Orphanage { Name = "Porirua Refuge", Address = "21 Lyttelton Ave", State = "Wellington", Country = "New Zealand" },
                    new Orphanage { Name = "Lower Hutt Orphanage", Address = "64 High St", State = "Wellington", Country = "New Zealand" },
                    new Orphanage { Name = "Christchurch East Home", Address = "78 Ferry Rd", State = "Canterbury", Country = "New Zealand" }
                };
                context.Orphanage.AddRange(orphanages);
                await context.SaveChangesAsync();

                var staffs = new Staff[]
                {
                    new Staff { FirstName = "Jane", LastName = "Doe", Role = "Caretaker", Phone = "021022393", Email = "jane.doe@example.com", OrphanageId = 1 },
                    new Staff { FirstName = "Michael", LastName = "Smith", Role = "Manager", Phone = "021045678", Email = "michael.smith@example.com", OrphanageId = 2 },
                    new Staff { FirstName = "Sarah", LastName = "Johnson", Role = "Caretaker", Phone = "021078945", Email = "sarah.johnson@example.com", OrphanageId = 3 },
                    new Staff { FirstName = "Robert", LastName = "Brown", Role = "Counselor", Phone = "021098765", Email = "robert.brown@example.com", OrphanageId = 4 },
                    new Staff { FirstName = "Emily", LastName = "Williams", Role = "Nurse", Phone = "021034567", Email = "emily.williams@example.com", OrphanageId = 5 },
                    new Staff { FirstName = "Daniel", LastName = "Martinez", Role = "Security", Phone = "021023456", Email = "daniel.martinez@example.com", OrphanageId = 6 },
                    new Staff { FirstName = "Jessica", LastName = "Garcia", Role = "Caretaker", Phone = "021045982", Email = "jessica.garcia@example.com", OrphanageId = 7 },
                    new Staff { FirstName = "Thomas", LastName = "Hernandez", Role = "Administrator", Phone = "021078954", Email = "thomas.hernandez@example.com", OrphanageId = 8 },
                    new Staff { FirstName = "Laura", LastName = "Lopez", Role = "Teacher", Phone = "021034789", Email = "laura.lopez@example.com", OrphanageId = 9 },
                    new Staff { FirstName = "Kevin", LastName = "Clark", Role = "Caretaker", Phone = "021098234", Email = "kevin.clark@example.com", OrphanageId = 10 }
                };
                context.Staff.AddRange(staffs);
                await context.SaveChangesAsync();

                var childrens = new Children[]
                {
                    new Children { Name = "Lucas", DateOfBirth = new DateTime(2016, 9, 11), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 10, 12) },
                    new Children { Name = "Amelia", DateOfBirth = new DateTime(2015, 12, 20), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 12, 1) },
                    new Children { Name = "Henry", DateOfBirth = new DateTime(2013, 10, 7), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2017, 11, 3) },
                    new Children { Name = "Isla", DateOfBirth = new DateTime(2016, 5, 16), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2021, 2, 28) },
                    new Children { Name = "Leo", DateOfBirth = new DateTime(2017, 3, 8), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2022, 6, 30) },
                    new Children { Name = "Grace", DateOfBirth = new DateTime(2014, 8, 19), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 10, 22) },
                    new Children { Name = "Jack", DateOfBirth = new DateTime(2015, 2, 24), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 3, 11) },
                    new Children { Name = "Ella", DateOfBirth = new DateTime(2016, 1, 30), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 8, 14) },
                    new Children { Name = "Harper", DateOfBirth = new DateTime(2017, 11, 9), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2021, 12, 9) },
                    new Children { Name = "Aria", DateOfBirth = new DateTime(2014, 6, 18), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 7, 23) }
                };
                context.Children.AddRange(childrens);
                await context.SaveChangesAsync();

                // Get the created user IDs for adoption records
                var parentUsers = await context.Users.ToListAsync();

                var adoptions = new Adoption[]
                {
                    new Adoption { AdoptionDate = new DateTime(2024, 7, 23), ParentId = parentUsers[0].Id, ChildrenId = 1, OrphanageId = 1 },
                    new Adoption { AdoptionDate = new DateTime(2023, 6, 15), ParentId = parentUsers[1].Id, ChildrenId = 2, OrphanageId = 2 },
                    new Adoption { AdoptionDate = new DateTime(2022, 5, 10), ParentId = parentUsers[2].Id, ChildrenId = 3, OrphanageId = 3 },
                    new Adoption { AdoptionDate = new DateTime(2021, 8, 18), ParentId = parentUsers[3].Id, ChildrenId = 4, OrphanageId = 4 },
                    new Adoption { AdoptionDate = new DateTime(2021, 5, 11), ParentId = parentUsers[4].Id, ChildrenId = 5, OrphanageId = 5 },
                    new Adoption { AdoptionDate = new DateTime(2020, 9, 25), ParentId = parentUsers[5].Id, ChildrenId = 6, OrphanageId = 6 },
                    new Adoption { AdoptionDate = new DateTime(2019, 3, 5), ParentId = parentUsers[6].Id, ChildrenId = 7, OrphanageId = 7 },
                    new Adoption { AdoptionDate = new DateTime(2018, 12, 12), ParentId = parentUsers[7].Id, ChildrenId = 8, OrphanageId = 8 },
                    new Adoption { AdoptionDate = new DateTime(2017, 7, 30), ParentId = parentUsers[8].Id, ChildrenId = 9, OrphanageId = 9 },
                    new Adoption { AdoptionDate = new DateTime(2016, 10, 22), ParentId = parentUsers[9].Id, ChildrenId = 10, OrphanageId = 10 }
                };
                context.Adoption.AddRange(adoptions);
                await context.SaveChangesAsync();

                logger.LogInformation("DatabaseStartup: Data seeded successfully!");
            }
        }
    }
}