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
                    new Parent { FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1999, 3, 23), Phone = "02102333333", Email = "JohnDoe@gmail.com", MartialStatus = "Single", Address = "New Lynn", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1985, 7, 12), Phone = "0210456789", Email = "alice.smith@example.com", MartialStatus = "Married", Address = "Epsom", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Bob", LastName = "Johnson", DateOfBirth = new DateTime(1990, 5, 14), Phone = "0210789654", Email = "bob.johnson@example.com", MartialStatus = "Divorced", Address = "Mt Albert", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Carol", LastName = "Williams", DateOfBirth = new DateTime(1978, 11, 25), Phone = "0210567345", Email = "carol.w@example.com", MartialStatus = "Single", Address = "Manukau", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "David", LastName = "Brown", DateOfBirth = new DateTime(1992, 2, 19), Phone = "0210452389", Email = "david.b@example.com", MartialStatus = "Married", Address = "Henderson", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Emily", LastName = "Clark", DateOfBirth = new DateTime(1983, 10, 5), Phone = "0210987654", Email = "emily.c@example.com", MartialStatus = "Widowed", Address = "Takapuna", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Frank", LastName = "Evans", DateOfBirth = new DateTime(1987, 6, 20), Phone = "0210678945", Email = "frank.e@example.com", MartialStatus = "Married", Address = "Albany", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Grace", LastName = "Hall", DateOfBirth = new DateTime(1995, 8, 11), Phone = "0210563214", Email = "grace.h@example.com", MartialStatus = "Single", Address = "Papakura", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Henry", LastName = "Lewis", DateOfBirth = new DateTime(1980, 4, 15), Phone = "0210784321", Email = "henry.l@example.com", MartialStatus = "Married", Address = "Botany", BirthPlace = "New Zealand" },
                    new Parent { FirstName = "Isabel", LastName = "Martinez", DateOfBirth = new DateTime(1991, 12, 3), Phone = "0210458765", Email = "isabel.m@example.com", MartialStatus = "Single", Address = "Glenfield", BirthPlace = "New Zealand" }
                };
                Context.Parent.AddRange(parents);
                Context.SaveChanges();

                var orphanages = new Orphanage[]
                {
                    new Orphanage { Name = "Auckland Orphanage", Address = "111 Waitakere", State = "Auckland", Country = "New Zealand" },
                    new Orphanage { Name = "Wellington Haven", Address = "222 Thorndon", State = "Wellington", Country = "New Zealand" },
                    new Orphanage { Name = "Christchurch Shelter", Address = "333 Riccarton", State = "Christchurch", Country = "New Zealand" },
                    new Orphanage { Name = "Hamilton Home", Address = "444 Hamilton St", State = "Waikato", Country = "New Zealand" },
                    new Orphanage { Name = "Dunedin Refuge", Address = "555 George St", State = "Otago", Country = "New Zealand" },
                    new Orphanage { Name = "Nelson Haven", Address = "666 Trafalgar St", State = "Nelson", Country = "New Zealand" },
                    new Orphanage { Name = "Rotorua Shelter", Address = "777 Fenton St", State = "Bay of Plenty", Country = "New Zealand" },
                    new Orphanage { Name = "Tauranga Orphanage", Address = "888 Cameron Rd", State = "Bay of Plenty", Country = "New Zealand" },
                    new Orphanage { Name = "Napier Care", Address = "999 Tennyson St", State = "Hawke's Bay", Country = "New Zealand" },
                    new Orphanage { Name = "New Plymouth Haven", Address = "1010 Devon St", State = "Taranaki", Country = "New Zealand" }
                };
                Context.Orphanage.AddRange(orphanages);
                Context.SaveChanges();

                var staffs = new Staff[]
{
                    new Staff { FirstName = "Jane", LastName = "Doe", Role = "Caretaker", Phone = "021022393", Email = "JaneDoe@gmail.com", OrphanageId = 1 },
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
                Context.Staff.AddRange(staffs);
                Context.SaveChanges();

                var childrens = new Children[]
{
                    new Children { Name = "James", DateOfBirth = new DateTime(2014, 3, 23, 9, 15, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 4, 19, 8, 11, 0) },
                    new Children { Name = "Sophia", DateOfBirth = new DateTime(2015, 5, 10, 7, 30, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 6, 15, 9, 45, 0) },
                    new Children { Name = "Liam", DateOfBirth = new DateTime(2013, 8, 2, 11, 20, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2017, 7, 12, 10, 30, 0) },
                    new Children { Name = "Emma", DateOfBirth = new DateTime(2016, 2, 5, 5, 50, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 1, 18, 6, 0, 0) },
                    new Children { Name = "Oliver", DateOfBirth = new DateTime(2017, 10, 14, 8, 5, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2021, 3, 2, 7, 25, 0) },
                    new Children { Name = "Ava", DateOfBirth = new DateTime(2014, 11, 30, 6, 45, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 9, 25, 5, 40, 0) },
                    new Children { Name = "Ethan", DateOfBirth = new DateTime(2015, 6, 28, 4, 55, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2019, 4, 17, 2, 10, 0) },
                    new Children { Name = "Mia", DateOfBirth = new DateTime(2016, 7, 19, 12, 30, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2020, 12, 4, 1, 35, 0) },
                    new Children { Name = "Benjamin", DateOfBirth = new DateTime(2017, 1, 3, 10, 15, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2021, 5, 10, 9, 0, 0) },
                    new Children { Name = "Charlotte", DateOfBirth = new DateTime(2014, 4, 25, 6, 5, 0), BirthPlace = "New Zealand", DateOfAdmission = new DateTime(2018, 8, 29, 4, 50, 0) }
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
                    new Adoption { AdoptionDate = new DateTime(2016, 10, 22, 12, 25, 0), ParentId = 10, ChildrenId = 10,  OrphanageId = 10  }
                };
                Context.Adoption.AddRange(adoptions);
                Context.SaveChanges();

            }
        }
    }
}
