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
                // Get required services
                var context = serviceScope.ServiceProvider.GetService<SunriseShelterDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<SunriseShelterUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var logger = serviceScope.ServiceProvider.GetService<ILogger<DatabaseStartup>>();

                logger.LogInformation("DatabaseStartup: Starting database seeding...");

                // Make sure database exists (creates it if missing)
                await context.Database.EnsureCreatedAsync();

                // Check if data already exists
                if (context.Users.Any() && context.Orphanage.Any() && context.Staff.Any() && context.Children.Any() && context.Adoption.Any())
                {
                    logger.LogInformation("DatabaseStartup: Data already exists. Skipping seed.");
                    return;
                }

                logger.LogInformation("DatabaseStartup: Seeding data...");

                // --- Users ---
                var parents = new SunriseShelterUser[]
                {
                    new SunriseShelterUser { UserName = "jack.taylor@example.com", Email = "jack.taylor@example.com", FirstName = "Jack", LastName = "Taylor", DateOfBirth = new DateTime(1988, 9, 10), PhoneNumber = "0210123456", MaritalStatus = "Married", Address = "Mt Eden", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "olivia.king@example.com", Email = "olivia.king@example.com", FirstName = "Olivia", LastName = "King", DateOfBirth = new DateTime(1993, 11, 2), PhoneNumber = "0210234567", MaritalStatus = "Single", Address = "Onehunga", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "ethan.scott@example.com", Email = "ethan.scott@example.com", FirstName = "Ethan", LastName = "Scott", DateOfBirth = new DateTime(1985, 1, 18), PhoneNumber = "0210345678", MaritalStatus = "Divorced", Address = "Mt Roskill", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lily.green@example.com", Email = "lily.green@example.com", FirstName = "Lily", LastName = "Green", DateOfBirth = new DateTime(1979, 4, 25), PhoneNumber = "0210456789", MaritalStatus = "Married", Address = "Remuera", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "noah.walker@example.com", Email = "noah.walker@example.com", FirstName = "Noah", LastName = "Walker", DateOfBirth = new DateTime(1990, 6, 12), PhoneNumber = "0210567890", MaritalStatus = "Single", Address = "Birkenhead", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "mia.hall@example.com", Email = "mia.hall@example.com", FirstName = "Mia", LastName = "Hall", DateOfBirth = new DateTime(1984, 8, 5), PhoneNumber = "0210678901", MaritalStatus = "Widowed", Address = "Mt Wellington", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lucas.allen@example.com", Email = "lucas.allen@example.com", FirstName = "Lucas", LastName = "Allen", DateOfBirth = new DateTime(1991, 3, 14), PhoneNumber = "0210789012", MaritalStatus = "Married", Address = "New Lynn", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "amelia.young@example.com", Email = "amelia.young@example.com", FirstName = "Amelia", LastName = "Young", DateOfBirth = new DateTime(1987, 5, 30), PhoneNumber = "0210890123", MaritalStatus = "Single", Address = "Epsom", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "mason.harris@example.com", Email = "mason.harris@example.com", FirstName = "Mason", LastName = "Harris", DateOfBirth = new DateTime(1982, 7, 21), PhoneNumber = "0210901234", MaritalStatus = "Married", Address = "Mt Albert", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "ella.martin@example.com", Email = "ella.martin@example.com", FirstName = "Ella", LastName = "Martin", DateOfBirth = new DateTime(1995, 9, 11), PhoneNumber = "0211012345", MaritalStatus = "Single", Address = "Manukau", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "james.young@example.com", Email = "james.young@example.com", FirstName = "James", LastName = "Young", DateOfBirth = new DateTime(1975, 1, 5), PhoneNumber = "0211111121", MaritalStatus = "Divorced", Address = "Palmerston North", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "charlotte.king@example.com", Email = "charlotte.king@example.com", FirstName = "Charlotte", LastName = "King", DateOfBirth = new DateTime(1994, 2, 16), PhoneNumber = "0211111122", MaritalStatus = "Single", Address = "Whanganui", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "alexander.scott@example.com", Email = "alexander.scott@example.com", FirstName = "Alexander", LastName = "Scott", DateOfBirth = new DateTime(1981, 8, 9), PhoneNumber = "0211111123", MaritalStatus = "Married", Address = "Gisborne", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "amelia.green@example.com", Email = "amelia.green@example.com", FirstName = "Amelia", LastName = "Green", DateOfBirth = new DateTime(1989, 3, 25), PhoneNumber = "0211111124", MaritalStatus = "Single", Address = "Masterton", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "daniel.adams@example.com", Email = "daniel.adams@example.com", FirstName = "Daniel", LastName = "Adams", DateOfBirth = new DateTime(1978, 12, 30), PhoneNumber = "0211111125", MaritalStatus = "Married", Address = "Kerikeri", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "sophia.ward@example.com", Email = "sophia.ward@example.com", FirstName = "Sophia", LastName = "Ward", DateOfBirth = new DateTime(1992, 5, 11), PhoneNumber = "0211111126", MaritalStatus = "Married", Address = "Cambridge", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "michael.turner@example.com", Email = "michael.turner@example.com", FirstName = "Michael", LastName = "Turner", DateOfBirth = new DateTime(1986, 9, 17), PhoneNumber = "0211111127", MaritalStatus = "Divorced", Address = "Hastings", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "ella.parker@example.com", Email = "ella.parker@example.com", FirstName = "Ella", LastName = "Parker", DateOfBirth = new DateTime(1995, 7, 4), PhoneNumber = "0211111128", MaritalStatus = "Single", Address = "New Plymouth", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "benjamin.carter@example.com", Email = "benjamin.carter@example.com", FirstName = "Benjamin", LastName = "Carter", DateOfBirth = new DateTime(1983, 2, 22), PhoneNumber = "0211111129", MaritalStatus = "Married", Address = "Timaru", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "grace.mitchell@example.com", Email = "grace.mitchell@example.com", FirstName = "Grace", LastName = "Mitchell", DateOfBirth = new DateTime(1988, 11, 1), PhoneNumber = "0211111130", MaritalStatus = "Widowed", Address = "Queenstown", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lucas.harris@example.com", Email = "lucas.harris@example.com", FirstName = "Lucas", LastName = "Harris", DateOfBirth = new DateTime(1979, 10, 18), PhoneNumber = "0211111131", MaritalStatus = "Married", Address = "Blenheim", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "zoe.phillips@example.com", Email = "zoe.phillips@example.com", FirstName = "Zoe", LastName = "Phillips", DateOfBirth = new DateTime(1996, 1, 12), PhoneNumber = "0211111132", MaritalStatus = "Single", Address = "Wanaka", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "henry.evans@example.com", Email = "henry.evans@example.com", FirstName = "Henry", LastName = "Evans", DateOfBirth = new DateTime(1985, 6, 29), PhoneNumber = "0211111133", MaritalStatus = "Married", Address = "Greymouth", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "lily.richards@example.com", Email = "lily.richards@example.com", FirstName = "Lily", LastName = "Richards", DateOfBirth = new DateTime(1982, 9, 15), PhoneNumber = "0211111134", MaritalStatus = "Single", Address = "Taupo", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "jack.cooper@example.com", Email = "jack.cooper@example.com", FirstName = "Jack", LastName = "Cooper", DateOfBirth = new DateTime(1990, 4, 2), PhoneNumber = "0211111135", MaritalStatus = "Divorced", Address = "Oamaru", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "aria.hughes@example.com", Email = "aria.hughes@example.com", FirstName = "Aria", LastName = "Hughes", DateOfBirth = new DateTime(1987, 3, 6), PhoneNumber = "0211111136", MaritalStatus = "Married", Address = "Ashburton", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "samuel.ward@example.com", Email = "samuel.ward@example.com", FirstName = "Samuel", LastName = "Ward", DateOfBirth = new DateTime(1984, 8, 24), PhoneNumber = "0211111137", MaritalStatus = "Married", Address = "Levin", BirthPlace = "New Zealand", EmailConfirmed = true },
                    new SunriseShelterUser { UserName = "chloe.bennett@example.com", Email = "chloe.bennett@example.com", FirstName = "Chloe", LastName = "Bennett", DateOfBirth = new DateTime(1992, 12, 19), PhoneNumber = "0211111138", MaritalStatus = "Single", Address = "Rotorua", BirthPlace = "New Zealand", EmailConfirmed = true }
                };

                foreach (var parent in parents)
                {
                    var result = await userManager.CreateAsync(parent, "Password123!");
                    if (result.Succeeded) await userManager.AddToRoleAsync(parent, "Parent");
                }

                // --- Orphanages ---
                var orphanages = new Orphanage[]
                {
                    new Orphanage { Name = "Auckland Hope Orphanage", Address = "Queen St Auckland", State = "Auckland", Country = "New Zealand" },
                    new Orphanage { Name = "Wellington Care Home", Address = "Lambton Quay Wellington", State = "Wellington", Country = "New Zealand" },
                    new Orphanage { Name = "Christchurch Family Shelter", Address = "Cashel St Christchurch", State = "Canterbury", Country = "New Zealand" },
                    new Orphanage { Name = "Hamilton Childrens Haven", Address = "Victoria St Hamilton", State = "Waikato", Country = "New Zealand" },
                    new Orphanage { Name = "Dunedin Sunshine Home", Address = "Stuart St Dunedin", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Tauranga Safe Haven", Address = "Cameron Rd Tauranga", State = "Bay of Plenty", Country = "New Zealand" },
                    new Orphanage { Name = "Rotorua Little Angels", Address = "Fenton St Rotorua", State = "Bay of Plenty", Country = "New Zealand" },
                    new Orphanage { Name = "Palmerston North Kids Home", Address = "Main St Palmerston North", State = "Manawatu Wanganui", Country = "New Zealand" },
                    new Orphanage { Name = "Napier Family Care", Address = "Hastings St Napier", State = "Hawkes Bay", Country = "New Zealand" },
                    new Orphanage { Name = "Invercargill Childrens Shelter", Address = "Dee St Invercargill", State = "Southland", Country = "New Zealand" },
                    new Orphanage { Name = "Whangarei Hope Home", Address = "Bank St Whangarei", State = "Northland", Country = "New Zealand" },
                    new Orphanage { Name = "Queenstown Family Haven", Address = "Beach St Queenstown", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Gisborne Kids Care", Address = "Reads Quay Gisborne", State = "Gisborne", Country = "New Zealand" },
                    new Orphanage { Name = "Taupo Little Stars", Address = "Tongariro St Taupo", State = "Waikato", Country = "New Zealand" },
                    new Orphanage { Name = "Blenheim Safe Haven", Address = "High St Blenheim", State = "Marlborough", Country = "New Zealand" },
                    new Orphanage { Name = "New Plymouth Childrens Home", Address = "Devon St New Plymouth", State = "Taranaki", Country = "New Zealand" },
                    new Orphanage { Name = "Hastings Family Shelter", Address = "Russell St Hastings", State = "Hawkes Bay", Country = "New Zealand" },
                    new Orphanage { Name = "Palmerston Hope Home", Address = "Church St Palmerston", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Cambridge Kids Haven", Address = "Victoria St Cambridge", State = "Waikato", Country = "New Zealand" },
                    new Orphanage { Name = "Ashburton Family Care", Address = "Tancred St Ashburton", State = "Canterbury", Country = "New Zealand" },
                    new Orphanage { Name = "Levin Childrens Haven", Address = "Oxford St Levin", State = "Manawatu Wanganui", Country = "New Zealand" },
                    new Orphanage { Name = "Rotorua Safe Home", Address = "Tutanekai St Rotorua", State = "Bay of Plenty", Country = "New Zealand" },
                    new Orphanage { Name = "Queenstown Little Angels", Address = "Shotover St Queenstown", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Whanganui Family Shelter", Address = "Victoria Ave Whanganui", State = "Manawatu Wanganui", Country = "New Zealand" },
                    new Orphanage { Name = "Timaru Hope Home", Address = "King St Timaru", State = "Canterbury", Country = "New Zealand" },
                    new Orphanage { Name = "Wanaka Kids Haven", Address = "Ardmore St Wanaka", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Kerikeri Family Care", Address = "Cobham Rd Kerikeri", State = "Northland", Country = "New Zealand" },
                    new Orphanage { Name = "Oamaru Little Stars", Address = "Thames St Oamaru", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Bluff Safe Haven", Address = "Gore St Bluff", State = "Southland", Country = "New Zealand" },
                    new Orphanage { Name = "Napier Little Angels", Address = "Station St Napier", State = "Hawkes Bay", Country = "New Zealand" },
                    new Orphanage { Name = "Hamilton Hope Home", Address = "Victoria St Hamilton", State = "Waikato", Country = "New Zealand" }
                };

                context.Orphanage.AddRange(orphanages);
                await context.SaveChangesAsync();



                // --- Staffs ---
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

                context.Staff.AddRange(staffs);
                await context.SaveChangesAsync();

                // --- Childrens ---
                var childrens = new Children[]
                {
                    new Children { Name = "Olivia", Gender = "Female", DateOfBirth = new DateTime(2016, 3, 12), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2022, 6, 1), Status = "Available", OrphanageId = 1 },
                    new Children { Name = "Liam", Gender = "Male", DateOfBirth = new DateTime(2015, 6, 23), BirthPlace = "Australia", DateOfAdmission = new DateTime(2021, 8, 15), Status = "In Process", OrphanageId = 2 },
                    new Children { Name = "Emma", Gender = "Female", DateOfBirth = new DateTime(2017, 11, 2), BirthPlace = "Fiji", DateOfAdmission = new DateTime(2023, 1, 20), Status = "Available", OrphanageId = 3 },
                    new Children { Name = "Noah", Gender = "Male", DateOfBirth = new DateTime(2014, 1, 15), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 3, 5), Status = "Adopted", OrphanageId = 4 },
                    new Children { Name = "Ava", Gender = "Female", DateOfBirth = new DateTime(2018, 5, 19), BirthPlace = "Samoa", DateOfAdmission = new DateTime(2023, 9, 10), Status = "Available", OrphanageId = 5 },
                    new Children { Name = "Ethan", Gender = "Male", DateOfBirth = new DateTime(2013, 9, 30), BirthPlace = "Tonga", DateOfAdmission = new DateTime(2019, 11, 12), Status = "Adopted", OrphanageId = 6 },
                    new Children { Name = "Sophia", Gender = "Female", DateOfBirth = new DateTime(2016, 12, 5), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2022, 4, 8), Status = "Available", OrphanageId = 7 },
                    new Children { Name = "Mason", Gender = "Male", DateOfBirth = new DateTime(2015, 8, 21), BirthPlace = "Australia", DateOfAdmission = new DateTime(2021, 7, 2), Status = "In Process", OrphanageId = 8 },
                    new Children { Name = "Isabella", Gender = "Female", DateOfBirth = new DateTime(2017, 4, 17), BirthPlace = "Fiji", DateOfAdmission = new DateTime(2023, 2, 14), Status = "Available", OrphanageId = 9 },
                    new Children { Name = "Lucas", Gender = "Male", DateOfBirth = new DateTime(2014, 2, 9), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 9, 1), Status = "Adopted", OrphanageId = 10 },
                    new Children { Name = "Mia", Gender = "Female", DateOfBirth = new DateTime(2018, 7, 14), BirthPlace = "CookIslands", DateOfAdmission = new DateTime(2023, 11, 7), Status = "Available", OrphanageId = 11 },
                    new Children { Name = "Logan", Gender = "Male", DateOfBirth = new DateTime(2013, 10, 27), BirthPlace = "Tonga", DateOfAdmission = new DateTime(2019, 5, 19), Status = "Adopted", OrphanageId = 12 },
                    new Children { Name = "Charlotte", Gender = "Female", DateOfBirth = new DateTime(2016, 1, 6), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2022, 3, 22), Status = "Available", OrphanageId = 13 },
                    new Children { Name = "Oliver", Gender = "Male", DateOfBirth = new DateTime(2015, 3, 29), BirthPlace = "Australia", DateOfAdmission = new DateTime(2021, 6, 30), Status = "In Process", OrphanageId = 14 },
                    new Children { Name = "Amelia", Gender = "Female", DateOfBirth = new DateTime(2017, 9, 8), BirthPlace = "Fiji", DateOfAdmission = new DateTime(2023, 4, 18), Status = "Available", OrphanageId = 15 },
                    new Children { Name = "Elijah", Gender = "Male", DateOfBirth = new DateTime(2014, 11, 20), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 12, 1), Status = "Adopted", OrphanageId = 16 },
                    new Children { Name = "Harper", Gender = "Female", DateOfBirth = new DateTime(2018, 2, 3), BirthPlace = "Samoa", DateOfAdmission = new DateTime(2023, 10, 6), Status = "Available", OrphanageId = 17 },
                    new Children { Name = "Aiden", Gender = "Male", DateOfBirth = new DateTime(2013, 6, 11), BirthPlace = "Tonga", DateOfAdmission = new DateTime(2019, 9, 23), Status = "Adopted", OrphanageId = 18 },
                    new Children { Name = "Ella", Gender = "Female", DateOfBirth = new DateTime(2016, 8, 25), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2022, 7, 4), Status = "In Process", OrphanageId = 19 },
                    new Children { Name = "Jackson", Gender = "Male", DateOfBirth = new DateTime(2015, 12, 13), BirthPlace = "Australia", DateOfAdmission = new DateTime(2021, 11, 9), Status = "Available", OrphanageId = 20 },
                    new Children { Name = "Scarlett", Gender = "Female", DateOfBirth = new DateTime(2017, 5, 1), BirthPlace = "Fiji", DateOfAdmission = new DateTime(2023, 3, 11), Status = "Available", OrphanageId = 21 },
                    new Children { Name = "Henry", Gender = "Male", DateOfBirth = new DateTime(2014, 7, 7), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 8, 24), Status = "Adopted", OrphanageId = 22 },
                    new Children { Name = "Aria", Gender = "Female", DateOfBirth = new DateTime(2018, 10, 16), BirthPlace = "CookIslands", DateOfAdmission = new DateTime(2023, 12, 2), Status = "Available", OrphanageId = 23 },
                    new Children { Name = "Sebastian", Gender = "Male", DateOfBirth = new DateTime(2013, 4, 4), BirthPlace = "Tonga", DateOfAdmission = new DateTime(2019, 6, 20), Status = "Adopted", OrphanageId = 22 },
                    new Children { Name = "Victoria", Gender = "Female", DateOfBirth = new DateTime(2016, 6, 22), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2022, 5, 15), Status = "Available", OrphanageId = 14 },
                    new Children { Name = "Wyatt", Gender = "Male", DateOfBirth = new DateTime(2015, 9, 9), BirthPlace = "Australia", DateOfAdmission = new DateTime(2021, 10, 28), Status = "In Process", OrphanageId = 13 },
                    new Children { Name = "Zoe", Gender = "Female", DateOfBirth = new DateTime(2017, 2, 28), BirthPlace = "Fiji", DateOfAdmission = new DateTime(2023, 5, 9), Status = "Available", OrphanageId = 12 },
                    new Children { Name = "Levi", Gender = "Male", DateOfBirth = new DateTime(2014, 5, 18), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 7, 13), Status = "Adopted", OrphanageId = 11 },
                    new Children { Name = "Hannah", Gender = "Female", DateOfBirth = new DateTime(2018, 12, 30), BirthPlace = "Samoa", DateOfAdmission = new DateTime(2024, 1, 5), Status = "Available", OrphanageId = 23 },
                    new Children { Name = "Owen", Gender = "Male", DateOfBirth = new DateTime(2013, 1, 2), BirthPlace = "Tonga", DateOfAdmission = new DateTime(2019, 4, 14), Status = "Adopted", OrphanageId = 15 },
                };

                context.Children.AddRange(childrens);
                await context.SaveChangesAsync();

                // Get the created parent users after seeding them
                var parentUsers = await context.Users.ToListAsync();

                // --- Adoptions ---
                var adoptions = new Adoption[]
                {
                    new Adoption { AdoptionDate = new DateTime(2023, 6, 12), ApplicationDate = new DateTime(2023, 5, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "jack.taylor@example.com").Id, ChildrenId = 1 },
                    new Adoption { AdoptionDate = new DateTime(2023, 8, 2), ApplicationDate = new DateTime(2023, 7, 10), Status = "Approved", ParentId = parentUsers.First(u => u.Email == "olivia.king@example.com").Id, ChildrenId = 2 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2023, 9, 20), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "ethan.scott@example.com").Id, ChildrenId = 3 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2023, 10, 5), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "lily.green@example.com").Id, ChildrenId = 4 },
                    new Adoption { AdoptionDate = new DateTime(2023, 11, 15), ApplicationDate = new DateTime(2023, 10, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "noah.walker@example.com").Id, ChildrenId = 5 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2023, 12, 12), Status = "Rejected", ParentId = parentUsers.First(u => u.Email == "mia.hall@example.com").Id, ChildrenId = 6 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 1, 25), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "lucas.allen@example.com").Id, ChildrenId = 7 },
                    new Adoption { AdoptionDate = new DateTime(2024, 2, 14), ApplicationDate = new DateTime(2024, 1, 5), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "amelia.young@example.com").Id, ChildrenId = 8 },
                    new Adoption { AdoptionDate = new DateTime(2024, 3, 2), ApplicationDate = new DateTime(2024, 2, 1), Status = "Approved", ParentId = parentUsers.First(u => u.Email == "mason.harris@example.com").Id, ChildrenId = 9 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 3, 18), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "ella.martin@example.com").Id, ChildrenId = 10 },
                    new Adoption { AdoptionDate = new DateTime(2024, 4, 5), ApplicationDate = new DateTime(2024, 3, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "james.young@example.com").Id, ChildrenId = 11 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 4, 20), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "charlotte.king@example.com").Id, ChildrenId = 12 },
                    new Adoption { AdoptionDate = new DateTime(2024, 5, 15), ApplicationDate = new DateTime(2024, 4, 10), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "alexander.scott@example.com").Id, ChildrenId = 13 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 6, 1), Status = "Rejected", ParentId = parentUsers.First(u => u.Email == "amelia.green@example.com").Id, ChildrenId = 14 },
                    new Adoption { AdoptionDate = new DateTime(2024, 6, 28), ApplicationDate = new DateTime(2024, 6, 10), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "daniel.adams@example.com").Id, ChildrenId = 15 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 7, 12), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "sophia.ward@example.com").Id, ChildrenId = 16 },
                    new Adoption { AdoptionDate = new DateTime(2024, 8, 7), ApplicationDate = new DateTime(2024, 7, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "michael.turner@example.com").Id, ChildrenId = 17 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 8, 30), Status = "Approved", ParentId = parentUsers.First(u => u.Email == "ella.parker@example.com").Id, ChildrenId = 18 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 9, 15), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "benjamin.carter@example.com").Id, ChildrenId = 19 },
                    new Adoption { AdoptionDate = new DateTime(2024, 10, 2), ApplicationDate = new DateTime(2024, 9, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "grace.mitchell@example.com").Id, ChildrenId = 20 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 10, 20), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "lucas.harris@example.com").Id, ChildrenId = 21 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 11, 1), Status = "Rejected", ParentId = parentUsers.First(u => u.Email == "zoe.phillips@example.com").Id, ChildrenId = 22 },
                    new Adoption { AdoptionDate = new DateTime(2024, 11, 25), ApplicationDate = new DateTime(2024, 11, 5), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "henry.evans@example.com").Id, ChildrenId = 23 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2024, 12, 12), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "lily.richards@example.com").Id, ChildrenId = 24 },
                    new Adoption { AdoptionDate = new DateTime(2025, 1, 15), ApplicationDate = new DateTime(2025, 1, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "jack.cooper@example.com").Id, ChildrenId = 25 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2025, 2, 10), Status = "Approved", ParentId = parentUsers.First(u => u.Email == "aria.hughes@example.com").Id, ChildrenId = 26 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2025, 2, 25), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "samuel.ward@example.com").Id, ChildrenId = 27 },
                    new Adoption { AdoptionDate = new DateTime(2025, 3, 5), ApplicationDate = new DateTime(2025, 2, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "chloe.bennett@example.com").Id, ChildrenId = 28 },
                    new Adoption { AdoptionDate = null, ApplicationDate = new DateTime(2025, 3, 20), Status = "Pending", ParentId = parentUsers.First(u => u.Email == "jack.taylor@example.com").Id, ChildrenId = 29 }, // reuse parent
                    new Adoption { AdoptionDate = new DateTime(2025, 4, 1), ApplicationDate = new DateTime(2025, 3, 1), Status = "Completed", ParentId = parentUsers.First(u => u.Email == "olivia.king@example.com").Id, ChildrenId = 30 } // reuse parent
                };

                context.Adoption.AddRange(adoptions);
                await context.SaveChangesAsync();
            }
        }
    }
}
