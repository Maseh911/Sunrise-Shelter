// ... your using statements remain unchanged

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;

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

                if (context.Users.Any() && context.Orphanage.Any() && context.Staff.Any() && context.Children.Any() && context.Adoption.Any())
                {
                    logger.LogInformation("DatabaseStartup: Data already exists. Skipping seed.");
                    return;
                }

                logger.LogInformation("DatabaseStartup: Seeding data...");

                // --- Users ---
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
                    new SunriseShelterUser { UserName = "ella.martin@example.com", Email = "ella.martin@example.com", FirstName = "Ella", LastName = "Martin", DateOfBirth = new DateTime(1995, 9, 11), PhoneNumber = "0211012345", MartialStatus = "Single", Address = "Manukau", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "james.young@example.com", Email = "james.young@example.com", FirstName = "James", LastName = "Young", DateOfBirth = new DateTime(1975, 1, 5), PhoneNumber = "0211111121", MartialStatus = "Divorced", Address = "Palmerston North", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "charlotte.king@example.com", Email = "charlotte.king@example.com", FirstName = "Charlotte", LastName = "King", DateOfBirth = new DateTime(1994, 2, 16), PhoneNumber = "0211111122", MartialStatus = "Single", Address = "Whanganui", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "alexander.scott@example.com", Email = "alexander.scott@example.com", FirstName = "Alexander", LastName = "Scott", DateOfBirth = new DateTime(1981, 8, 9), PhoneNumber = "0211111123", MartialStatus = "Married", Address = "Gisborne", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "amelia.green@example.com", Email = "amelia.green@example.com", FirstName = "Amelia", LastName = "Green", DateOfBirth = new DateTime(1989, 3, 25), PhoneNumber = "0211111124", MartialStatus = "Single", Address = "Masterton", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "daniel.adams@example.com", Email = "daniel.adams@example.com", FirstName = "Daniel", LastName = "Adams", DateOfBirth = new DateTime(1978, 12, 30), PhoneNumber = "0211111125", MartialStatus = "Married", Address = "Kerikeri", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "sophia.ward@example.com", Email = "sophia.ward@example.com", FirstName = "Sophia", LastName = "Ward", DateOfBirth = new DateTime(1992, 5, 11), PhoneNumber = "0211111126", MartialStatus = "Married", Address = "Cambridge", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "michael.turner@example.com", Email = "michael.turner@example.com", FirstName = "Michael", LastName = "Turner", DateOfBirth = new DateTime(1986, 9, 17), PhoneNumber = "0211111127", MartialStatus = "Divorced", Address = "Hastings", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "ella.parker@example.com", Email = "ella.parker@example.com", FirstName = "Ella", LastName = "Parker", DateOfBirth = new DateTime(1995, 7, 4), PhoneNumber = "0211111128", MartialStatus = "Single", Address = "New Plymouth", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "benjamin.carter@example.com", Email = "benjamin.carter@example.com", FirstName = "Benjamin", LastName = "Carter", DateOfBirth = new DateTime(1983, 2, 22), PhoneNumber = "0211111129", MartialStatus = "Married", Address = "Timaru", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "grace.mitchell@example.com", Email = "grace.mitchell@example.com", FirstName = "Grace", LastName = "Mitchell", DateOfBirth = new DateTime(1988, 11, 1), PhoneNumber = "0211111130", MartialStatus = "Widowed", Address = "Queenstown", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lucas.harris@example.com", Email = "lucas.harris@example.com", FirstName = "Lucas", LastName = "Harris", DateOfBirth = new DateTime(1979, 10, 18), PhoneNumber = "0211111131", MartialStatus = "Married", Address = "Blenheim", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "zoe.phillips@example.com", Email = "zoe.phillips@example.com", FirstName = "Zoe", LastName = "Phillips", DateOfBirth = new DateTime(1996, 1, 12), PhoneNumber = "0211111132", MartialStatus = "Single", Address = "Wanaka", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "henry.evans@example.com", Email = "henry.evans@example.com", FirstName = "Henry", LastName = "Evans", DateOfBirth = new DateTime(1985, 6, 29), PhoneNumber = "0211111133", MartialStatus = "Married", Address = "Greymouth", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lily.richards@example.com", Email = "lily.richards@example.com", FirstName = "Lily", LastName = "Richards", DateOfBirth = new DateTime(1982, 9, 15), PhoneNumber = "0211111134", MartialStatus = "Single", Address = "Taupo", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "jack.cooper@example.com", Email = "jack.cooper@example.com", FirstName = "Jack", LastName = "Cooper", DateOfBirth = new DateTime(1990, 4, 2), PhoneNumber = "0211111135", MartialStatus = "Divorced", Address = "Oamaru", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "aria.hughes@example.com", Email = "aria.hughes@example.com", FirstName = "Aria", LastName = "Hughes", DateOfBirth = new DateTime(1987, 3, 6), PhoneNumber = "0211111136", MartialStatus = "Married", Address = "Ashburton", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "samuel.ward@example.com", Email = "samuel.ward@example.com", FirstName = "Samuel", LastName = "Ward", DateOfBirth = new DateTime(1984, 8, 24), PhoneNumber = "0211111137", MartialStatus = "Married", Address = "Levin", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "chloe.bennett@example.com", Email = "chloe.bennett@example.com", FirstName = "Chloe", LastName = "Bennett", DateOfBirth = new DateTime(1992, 12, 19), PhoneNumber = "0211111138", MartialStatus = "Single", Address = "Rotorua", BirthPlace = "New Zealand", EmailConfirmed = true }
                };

                foreach (var parent in parents)
                {
                    var result = await userManager.CreateAsync(parent, "Password123!");
                    if (result.Succeeded) await userManager.AddToRoleAsync(parent, "Parent");
                }

                // --- Orphanages ---
                var orphanages = new Orphanage[]
                {
    new Orphanage { Name = "Auckland Hope Orphanage", Address = "12 Queen St, Auckland", State = "Auckland", Country = "New Zealand" },
    new Orphanage { Name = "Wellington Care Home", Address = "45 Lambton Quay, Wellington", State = "Wellington", Country = "New Zealand" },
    new Orphanage { Name = "Christchurch Family Shelter", Address = "78 Cashel St, Christchurch", State = "Canterbury", Country = "New Zealand" },
    new Orphanage { Name = "Hamilton Children's Haven", Address = "23 Victoria St, Hamilton", State = "Waikato", Country = "New Zealand" },
    new Orphanage { Name = "Dunedin Sunshine Home", Address = "5 Stuart St, Dunedin", State = "Otago", Country = "New Zealand" },
    new Orphanage { Name = "Tauranga Safe Haven", Address = "101 Cameron Rd, Tauranga", State = "Bay of Plenty", Country = "New Zealand" },
    new Orphanage { Name = "Rotorua Little Angels", Address = "32 Fenton St, Rotorua", State = "Bay of Plenty", Country = "New Zealand" },
    new Orphanage { Name = "Palmerston North Kids' Home", Address = "88 Main St, Palmerston North", State = "Manawatu-Wanganui", Country = "New Zealand" },
    new Orphanage { Name = "Napier Family Care", Address = "14 Hastings St, Napier", State = "Hawke's Bay", Country = "New Zealand" },
    new Orphanage { Name = "Invercargill Children's Shelter", Address = "66 Dee St, Invercargill", State = "Southland", Country = "New Zealand" },
    new Orphanage { Name = "Whangarei Hope Home", Address = "77 Bank St, Whangarei", State = "Northland", Country = "New Zealand" },
    new Orphanage { Name = "Queenstown Family Haven", Address = "2 Beach St, Queenstown", State = "Otago", Country = "New Zealand" },
    new Orphanage { Name = "Gisborne Kids' Care", Address = "34 Reads Quay, Gisborne", State = "Gisborne", Country = "New Zealand" },
    new Orphanage { Name = "Taupo Little Stars", Address = "56 Tongariro St, Taupo", State = "Waikato", Country = "New Zealand" },
    new Orphanage { Name = "Blenheim Safe Haven", Address = "19 High St, Blenheim", State = "Marlborough", Country = "New Zealand" },
    new Orphanage { Name = "New Plymouth Children's Home", Address = "21 Devon St, New Plymouth", State = "Taranaki", Country = "New Zealand" },
    new Orphanage { Name = "Hastings Family Shelter", Address = "9 Russell St, Hastings", State = "Hawke's Bay", Country = "New Zealand" },
    new Orphanage { Name = "Palmerston Hope Home", Address = "44 Church St, Palmerston", State = "Otago", Country = "New Zealand" },
    new Orphanage { Name = "Cambridge Kids' Haven", Address = "33 Victoria St, Cambridge", State = "Waikato", Country = "New Zealand" },
    new Orphanage { Name = "Ashburton Family Care", Address = "10 Tancred St, Ashburton", State = "Canterbury", Country = "New Zealand" },
    new Orphanage { Name = "Levin Children's Haven", Address = "55 Oxford St, Levin", State = "Manawatu-Wanganui", Country = "New Zealand" },
    new Orphanage { Name = "Rotorua Safe Home", Address = "12 Tutanekai St, Rotorua", State = "Bay of Plenty", Country = "New Zealand" },
    new Orphanage { Name = "Queenstown Little Angels", Address = "77 Shotover St, Queenstown", State = "Otago", Country = "New Zealand" },
    new Orphanage { Name = "Whanganui Family Shelter", Address = "88 Victoria Ave, Whanganui", State = "Manawatu-Wanganui", Country = "New Zealand" },
    new Orphanage { Name = "Timaru Hope Home", Address = "31 King St, Timaru", State = "Canterbury", Country = "New Zealand" },
    new Orphanage { Name = "Wanaka Kids' Haven", Address = "17 Ardmore St, Wanaka", State = "Otago", Country = "New Zealand" },
    new Orphanage { Name = "Kerikeri Family Care", Address = "24 Cobham Rd, Kerikeri", State = "Northland", Country = "New Zealand" },
    new Orphanage { Name = "Oamaru Little Stars", Address = "9 Thames St, Oamaru", State = "Otago", Country = "New Zealand" },
    new Orphanage { Name = "Bluff Safe Haven", Address = "33 Gore St, Bluff", State = "Southland", Country = "New Zealand" },
    new Orphanage { Name = "Napier Little Angels", Address = "42 Station St, Napier", State = "Hawke's Bay", Country = "New Zealand" },
    new Orphanage { Name = "Hamilton Hope Home", Address = "56 Victoria St, Hamilton", State = "Waikato", Country = "New Zealand" }
                };


                var staffs = new Staff[]
                {
    new Staff { FirstName = "Jane", LastName = "Doe", Role = "Caretaker", Phone = "0210123456", Email = "jane.doe@example.com", OrphanageId = 1 },
    new Staff { FirstName = "Michael", LastName = "Smith", Role = "Manager", Phone = "0210234567", Email = "michael.smith@example.com", OrphanageId = 2 },
    new Staff { FirstName = "Sarah", LastName = "Johnson", Role = "Caretaker", Phone = "0210345678", Email = "sarah.johnson@example.com", OrphanageId = 3 },
    new Staff { FirstName = "Robert", LastName = "Brown", Role = "Counselor", Phone = "0210456789", Email = "robert.brown@example.com", OrphanageId = 4 },
    new Staff { FirstName = "Emily", LastName = "Williams", Role = "Nurse", Phone = "0210567890", Email = "emily.williams@example.com", OrphanageId = 5 },
    new Staff { FirstName = "Daniel", LastName = "Martinez", Role = "Security", Phone = "0210678901", Email = "daniel.martinez@example.com", OrphanageId = 6 },
    new Staff { FirstName = "Jessica", LastName = "Garcia", Role = "Caretaker", Phone = "0210789012", Email = "jessica.garcia@example.com", OrphanageId = 7 },
    new Staff { FirstName = "Laura", LastName = "Lopez", Role = "Teacher", Phone = "0210890123", Email = "laura.lopez@example.com", OrphanageId = 8 },
    new Staff { FirstName = "Kevin", LastName = "Clark", Role = "Caretaker", Phone = "0210901234", Email = "kevin.clark@example.com", OrphanageId = 9 },
    new Staff { FirstName = "Sophia", LastName = "Lewis", Role = "Manager", Phone = "0211012345", Email = "sophia.lewis@example.com", OrphanageId = 10 },
    new Staff { FirstName = "James", LastName = "Walker", Role = "Teacher", Phone = "0211123456", Email = "james.walker@example.com", OrphanageId = 11 },
    new Staff { FirstName = "Olivia", LastName = "Hall", Role = "Nurse", Phone = "0211234567", Email = "olivia.hall@example.com", OrphanageId = 12 },
    new Staff { FirstName = "Ethan", LastName = "Allen", Role = "Counselor", Phone = "0211345678", Email = "ethan.allen@example.com", OrphanageId = 13 },
    new Staff { FirstName = "Mia", LastName = "Young", Role = "Caretaker", Phone = "0211456789", Email = "mia.young@example.com", OrphanageId = 14 },
    new Staff { FirstName = "Benjamin", LastName = "Scott", Role = "Caretaker", Phone = "0211567890", Email = "benjamin.scott@example.com", OrphanageId = 15 },
    new Staff { FirstName = "Charlotte", LastName = "Torres", Role = "Teacher", Phone = "0211678901", Email = "charlotte.torres@example.com", OrphanageId = 16 },
    new Staff { FirstName = "Alexander", LastName = "Nguyen", Role = "Manager", Phone = "0211789012", Email = "alexander.nguyen@example.com", OrphanageId = 17 },
    new Staff { FirstName = "Grace", LastName = "Hill", Role = "Nurse", Phone = "0211890123", Email = "grace.hill@example.com", OrphanageId = 18 },
    new Staff { FirstName = "Lucas", LastName = "Adams", Role = "Counselor", Phone = "0211901234", Email = "lucas.adams@example.com", OrphanageId = 19 },
    new Staff { FirstName = "Ava", LastName = "Baker", Role = "Caretaker", Phone = "0212012345", Email = "ava.baker@example.com", OrphanageId = 20 },
    new Staff { FirstName = "Mason", LastName = "Parker", Role = "Security", Phone = "0212123456", Email = "mason.parker@example.com", OrphanageId = 21 },
    new Staff { FirstName = "Harper", LastName = "Rivera", Role = "Administrator", Phone = "0212234567", Email = "harper.rivera@example.com", OrphanageId = 22 },
    new Staff { FirstName = "Elijah", LastName = "Mitchell", Role = "Caretaker", Phone = "0212345678", Email = "elijah.mitchell@example.com", OrphanageId = 23 },
    new Staff { FirstName = "Amelia", LastName = "Carter", Role = "Teacher", Phone = "0212456789", Email = "amelia.carter@example.com", OrphanageId = 24 },
    new Staff { FirstName = "Daniel", LastName = "Phillips", Role = "Manager", Phone = "0212567890", Email = "daniel.phillips@example.com", OrphanageId = 25 },
    new Staff { FirstName = "Sofia", LastName = "Evans", Role = "Nurse", Phone = "0212678901", Email = "sofia.evans@example.com", OrphanageId = 26 },
    new Staff { FirstName = "Henry", LastName = "Turner", Role = "Counselor", Phone = "0212789012", Email = "henry.turner@example.com", OrphanageId = 27 },
    new Staff { FirstName = "Ella", LastName = "Ramirez", Role = "Caretaker", Phone = "0212890123", Email = "ella.ramirez@example.com", OrphanageId = 28 },
    new Staff { FirstName = "Jackson", LastName = "Campbell", Role = "Security", Phone = "0212901234", Email = "jackson.campbell@example.com", OrphanageId = 29 },
    new Staff { FirstName = "Lily", LastName = "Roberts", Role = "Teacher", Phone = "0213012345", Email = "lily.roberts@example.com", OrphanageId = 30 },
                };

                // --- Children ---
                var childrens = new Children[30];
                for (int i = 0; i < 30; i++)
                {
                    childrens[i] = new Children
                    {
                        Name = $"Child{i + 1}",
                        Gender = i % 2 == 0 ? "Male" : "Female",
                        DateOfBirth = new DateTime(2013 + (i % 5), (i % 12) + 1, ((i * 2) % 28) + 1),
                        BirthPlace = "New Zealand",
                        DateOfAdmission = new DateTime(2018 + (i % 5), ((i + 6) % 12) + 1, ((i * 3) % 28) + 1),
                        Status = i % 3 == 0 ? "Available" : i % 3 == 1 ? "Adopted" : "In Process",
                        OrphanageId = (i % 30) + 1
                    };
                }
                context.Children.AddRange(childrens);
                await context.SaveChangesAsync();

                // --- Adoptions ---
                var parentUsers = await context.Users.ToListAsync();
                var adoptions = new Adoption[30];
                for (int i = 0; i < 30; i++)
                {
                    adoptions[i
