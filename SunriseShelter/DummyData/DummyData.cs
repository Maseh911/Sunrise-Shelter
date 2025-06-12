using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;
using Microsoft.CodeAnalysis;

namespace SunriseShelter.Data
{
    public class DatabaseStartup
    {
        public static void StartUp(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var Context = serviceScope.ServiceProvider.GetService<SunriseShelterDbContext>();
                Context.Database.EnsureCreated();

                if (Context.Parent.Any() || Context.Orphanage.Any() || Context.Staff.Any() || Context.Children.Any() || Context.Adoption.Any())
                {
                    return;
                }

                var parents = new Parent[]
                {
                    new Parent { FirstName = "Jack", LastName = "Taylor", DateOfBirth = new DateTime(1988, 9, 10), Phone = "0210123456", Email = "jack.taylor@example.com", MartialStatus = "Married", Address = "Mt Eden", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Olivia", LastName = "King", DateOfBirth = new DateTime(1993, 11, 2), Phone = "0210234567", Email = "olivia.king@example.com", MartialStatus = "Single", Address = "Onehunga", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Ethan", LastName = "Scott", DateOfBirth = new DateTime(1985, 1, 18), Phone = "0210345678", Email = "ethan.scott@example.com", MartialStatus = "Divorced", Address = "Mt Roskill", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Lily", LastName = "Green", DateOfBirth = new DateTime(1979, 4, 25), Phone = "0210456789", Email = "lily.green@example.com", MartialStatus = "Married", Address = "Remuera", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Noah", LastName = "Walker", DateOfBirth = new DateTime(1990, 6, 12), Phone = "0210567890", Email = "noah.walker@example.com", MartialStatus = "Single", Address = "Birkenhead", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Mia", LastName = "Hall", DateOfBirth = new DateTime(1984, 8, 5), Phone = "0210678901", Email = "mia.hall@example.com", MartialStatus = "Widowed", Address = "Mt Wellington", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Lucas", LastName = "Allen", DateOfBirth = new DateTime(1991, 3, 14), Phone = "0210789012", Email = "lucas.allen@example.com", MartialStatus = "Married", Address = "New Lynn", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Amelia", LastName = "Young", DateOfBirth = new DateTime(1987, 5, 30), Phone = "0210890123", Email = "amelia.young@example.com", MartialStatus = "Single", Address = "Epsom", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Mason", LastName = "Harris", DateOfBirth = new DateTime(1982, 7, 21), Phone = "0210901234", Email = "mason.harris@example.com", MartialStatus = "Married", Address = "Mt Albert", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Ella", LastName = "Martin", DateOfBirth = new DateTime(1995, 9, 11), Phone = "0211012345", Email = "ella.martin@example.com", MartialStatus = "Single", Address = "Manukau", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Oliver", LastName = "White", DateOfBirth = new DateTime(1989, 12, 7), Phone = "0211123456", Email = "oliver.white@example.com", MartialStatus = "Married", Address = "Henderson", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Chloe", LastName = "King", DateOfBirth = new DateTime(1992, 2, 22), Phone = "0211234567", Email = "chloe.king@example.com", MartialStatus = "Divorced", Address = "Takapuna", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "James", LastName = "Scott", DateOfBirth = new DateTime(1986, 10, 18), Phone = "0211345678", Email = "james.scott@example.com", MartialStatus = "Married", Address = "Albany", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Sophia", LastName = "Baker", DateOfBirth = new DateTime(1981, 8, 9), Phone = "0211456789", Email = "sophia.baker@example.com", MartialStatus = "Single", Address = "Papakura", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "William", LastName = "Evans", DateOfBirth = new DateTime(1993, 11, 15), Phone = "0211567890", Email = "william.evans@example.com", MartialStatus = "Married", Address = "Botany", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Isabella", LastName = "Collins", DateOfBirth = new DateTime(1990, 1, 3), Phone = "0211678901", Email = "isabella.collins@example.com", MartialStatus = "Single", Address = "Glenfield", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Henry", LastName = "Morgan", DateOfBirth = new DateTime(1983, 3, 29), Phone = "0211789012", Email = "henry.morgan@example.com", MartialStatus = "Married", Address = "New Lynn", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Grace", LastName = "Stewart", DateOfBirth = new DateTime(1987, 7, 10), Phone = "0211890123", Email = "grace.stewart@example.com", MartialStatus = "Single", Address = "Epsom", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Jack", LastName = "Reed", DateOfBirth = new DateTime(1994, 5, 5), Phone = "0211901234", Email = "jack.reed@example.com", MartialStatus = "Married", Address = "Mt Albert", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Ella", LastName = "Parker", DateOfBirth = new DateTime(1988, 4, 1), Phone = "0212012345", Email = "ella.parker@example.com", MartialStatus = "Single", Address = "Manukau", BirthPlace = "New Zealand" }
                };

                Context.Parent.AddRange(parents);
                Context.SaveChanges();

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
                    new Orphanage { Name = "Christchurch East Home", Address = "78 Ferry Rd", State = "Canterbury", Country = "New Zealand" },
                    new Orphanage { Name = "Dunedin Haven", Address = "35 George St", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Queenstown Shelter", Address = "9 Stanley St", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Te Awamutu Home", Address = "41 Alexandra St", State = "Waikato", Country = "New Zealand" },
                    new Orphanage { Name = "Kerikeri Orphanage", Address = "26 Hobson Ave", State = "Northland", Country = "New Zealand" },
                    new Orphanage { Name = "Cambridge Refuge", Address = "53 Victoria St", State = "Waikato", Country = "New Zealand" },
                    new Orphanage { Name = "Feilding Haven", Address = "19 Manchester St", State = "Manawatu-Wanganui", Country = "New Zealand" },
                    new Orphanage { Name = "Oamaru Shelter", Address = "14 Thames St", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Pukekohe Orphanage", Address = "30 Edinburgh St", State = "Auckland", Country = "New Zealand" },
                    new Orphanage { Name = "Whakatane Refuge", Address = "22 The Strand", State = "Bay of Plenty", Country = "New Zealand" },
                    new Orphanage { Name = "Warkworth Haven", Address = "48 Baxter St", State = "Auckland", Country = "New Zealand" }
                };
                Context.Orphanage.AddRange(orphanages);
                Context.SaveChanges();

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
                    new Staff { FirstName = "Kevin", LastName = "Clark", Role = "Caretaker", Phone = "021098234", Email = "kevin.clark@example.com", OrphanageId = 10 },
                    new Staff { FirstName = "Olivia", LastName = "Moore", Role = "Social Worker", Phone = "021112345", Email = "olivia.moore@example.com", OrphanageId = 11 },
                    new Staff { FirstName = "Ethan", LastName = "Taylor", Role = "Caretaker", Phone = "021221456", Email = "ethan.taylor@example.com", OrphanageId = 12 },
                    new Staff { FirstName = "Sophia", LastName = "Anderson", Role = "Cook", Phone = "021332567", Email = "sophia.anderson@example.com", OrphanageId = 13 },
                    new Staff { FirstName = "Liam", LastName = "Thomas", Role = "Caretaker", Phone = "021443678", Email = "liam.thomas@example.com", OrphanageId = 14 },
                    new Staff { FirstName = "Chloe", LastName = "Jackson", Role = "Nurse", Phone = "021554789", Email = "chloe.jackson@example.com", OrphanageId = 15 },
                    new Staff { FirstName = "Noah", LastName = "White", Role = "Counselor", Phone = "021665890", Email = "noah.white@example.com", OrphanageId = 16 },
                    new Staff { FirstName = "Ava", LastName = "Harris", Role = "Caretaker", Phone = "021776901", Email = "ava.harris@example.com", OrphanageId = 17 },
                    new Staff { FirstName = "James", LastName = "Martin", Role = "Security", Phone = "021887012", Email = "james.martin@example.com", OrphanageId = 18 },
                    new Staff { FirstName = "Lily", LastName = "Thompson", Role = "Administrator", Phone = "021998123", Email = "lily.thompson@example.com", OrphanageId = 19 },
                    new Staff { FirstName = "Benjamin", LastName = "Lee", Role = "Caretaker", Phone = "021019234", Email = "benjamin.lee@example.com", OrphanageId = 20 },
};
                Context.Staff.AddRange(staffs);
                Context.SaveChanges();

                var childrens = new Children[]
{
                    new Children { Name = "Lucas", DateOfBirth = new DateTime(2016, 9, 11, 3, 40, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 10, 12, 5, 15, 0) },
                    new Children { Name = "Amelia", DateOfBirth = new DateTime(2015, 12, 20, 6, 25, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 12, 1, 7, 10, 0) },
                    new Children { Name = "Henry", DateOfBirth = new DateTime(2013, 10, 7, 2, 30, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2017, 11, 3, 3, 45, 0) },
                    new Children { Name = "Isla", DateOfBirth = new DateTime(2016, 5, 16, 11, 5, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2021, 2, 28, 12, 0, 0) },
                    new Children { Name = "Leo", DateOfBirth = new DateTime(2017, 3, 8, 9, 15, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2022, 6, 30, 10, 5, 0) },
                    new Children { Name = "Grace", DateOfBirth = new DateTime(2014, 8, 19, 7, 0, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 10, 22, 6, 20, 0) },
                    new Children { Name = "Jack", DateOfBirth = new DateTime(2015, 2, 24, 1, 45, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 3, 11, 2, 10, 0) },
                    new Children { Name = "Ella", DateOfBirth = new DateTime(2016, 1, 30, 5, 50, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 8, 14, 4, 55, 0) },
                    new Children { Name = "Harper", DateOfBirth = new DateTime(2017, 11, 9, 6, 10, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2021, 12, 9, 5, 30, 0) },
                    new Children { Name = "Aria", DateOfBirth = new DateTime(2014, 6, 18, 8, 35, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 7, 23, 7, 40, 0) },
                    new Children { Name = "William", DateOfBirth = new DateTime(2015, 9, 5, 2, 20, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 10, 5, 3, 30, 0) },
                    new Children { Name = "Zoe", DateOfBirth = new DateTime(2016, 4, 21, 4, 0, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 11, 11, 5, 15, 0) },
                    new Children { Name = "Logan", DateOfBirth = new DateTime(2013, 12, 13, 9, 10, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2017, 12, 20, 10, 25, 0) },
                    new Children { Name = "Abigail", DateOfBirth = new DateTime(2015, 7, 26, 7, 35, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 9, 6, 6, 30, 0) },
                    new Children { Name = "Elijah", DateOfBirth = new DateTime(2016, 3, 17, 3, 50, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 2, 8, 4, 10, 0) },
                    new Children { Name = "Sophie", DateOfBirth = new DateTime(2014, 10, 28, 10, 45, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 11, 17, 9, 50, 0) },
                    new Children { Name = "Jacob", DateOfBirth = new DateTime(2015, 1, 6, 6, 5, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 1, 20, 7, 0, 0) },
                    new Children { Name = "Emily", DateOfBirth = new DateTime(2017, 2, 14, 12, 15, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2021, 4, 1, 11, 30, 0) },
                    new Children { Name = "Mason", DateOfBirth = new DateTime(2016, 6, 7, 11, 25, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 7, 2, 10, 50, 0) },
                    new Children { Name = "Lily", DateOfBirth = new DateTime(2014, 5, 12, 8, 0, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 6, 3, 9, 10, 0) }
};
                Context.Children.AddRange(childrens);
                Context.SaveChanges();

                var adoptions = new Adoption[]
                {
                    new Adoption { AdoptionDate = new DateTime(2024, 7, 23, 9, 15, 0), ParentId = 1, ChildrenId = 1, OrphanageId = 1 },
                    new Adoption { AdoptionDate = new DateTime(2023, 6, 15, 10, 30, 0), ParentId = 2, ChildrenId = 2,  OrphanageId = 2 },
                    new Adoption { AdoptionDate = new DateTime(2022, 5, 10, 14, 45, 0), ParentId = 3, ChildrenId = 3,  OrphanageId = 3  },
                    new Adoption { AdoptionDate = new DateTime(2021, 8, 18, 11, 20, 0), ParentId = 4, ChildrenId = 4,  OrphanageId = 4  },
                    new Adoption { AdoptionDate = new DateTime(2021, 5, 11, 12, 22, 0), ParentId = 5, ChildrenId = 5,  OrphanageId = 5  },
                    new Adoption { AdoptionDate = new DateTime(2020, 9, 25, 16, 5, 0), ParentId = 6, ChildrenId = 6,  OrphanageId = 6  },
                    new Adoption { AdoptionDate = new DateTime(2019, 3, 5, 13, 50, 0), ParentId = 7, ChildrenId = 7,  OrphanageId = 7  },
                    new Adoption { AdoptionDate = new DateTime(2018, 12, 12, 9, 10, 0), ParentId = 8, ChildrenId = 8,  OrphanageId = 8  },
                    new Adoption { AdoptionDate = new DateTime(2017, 7, 30, 15, 40, 0), ParentId = 9, ChildrenId = 9,  OrphanageId = 9  },
                    new Adoption { AdoptionDate = new DateTime(2016, 10, 22, 12, 25, 0), ParentId = 10, ChildrenId = 10,  OrphanageId = 10},
                    new Adoption { AdoptionDate = new DateTime(2024, 2, 14, 10, 30, 0), ParentId = 11, ChildrenId = 11, OrphanageId = 11 },
                    new Adoption { AdoptionDate = new DateTime(2023, 4, 5, 13, 20, 0), ParentId = 12, ChildrenId = 12, OrphanageId = 12 },
                    new Adoption { AdoptionDate = new DateTime(2022, 11, 19, 9, 45, 0), ParentId = 13, ChildrenId = 13, OrphanageId = 13 },
                    new Adoption { AdoptionDate = new DateTime(2021, 3, 28, 14, 10, 0), ParentId = 14, ChildrenId = 14, OrphanageId = 14 },
                    new Adoption { AdoptionDate = new DateTime(2020, 7, 16, 11, 5, 0), ParentId = 15, ChildrenId = 15, OrphanageId = 15 },
                    new Adoption { AdoptionDate = new DateTime(2019, 5, 3, 15, 30, 0), ParentId = 16, ChildrenId = 16, OrphanageId = 16 },
                    new Adoption { AdoptionDate = new DateTime(2018, 9, 9, 10, 0, 0), ParentId = 17, ChildrenId = 17, OrphanageId = 17 },
                    new Adoption { AdoptionDate = new DateTime(2017, 1, 21, 13, 15, 0), ParentId = 18, ChildrenId = 18, OrphanageId = 18 },
                    new Adoption { AdoptionDate = new DateTime(2016, 6, 11, 14, 40, 0), ParentId = 19, ChildrenId = 19, OrphanageId = 19 },
                    new Adoption { AdoptionDate = new DateTime(2015, 8, 4, 12, 55, 0), ParentId = 20, ChildrenId = 20, OrphanageId = 20 }
                };
                Context.Adoption.AddRange(adoptions);
                Context.SaveChanges();

            }
        }
    }
}
