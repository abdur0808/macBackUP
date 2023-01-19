using System;
using Microsoft.EntityFrameworkCore;
using VillaAPI.models;

namespace VillaAPI.Data
{
	public class ApplicationDbContext: DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // the name 'Villas' table will created in SQL Database
        public DbSet<Villa> Villas { get; set; }

        public DbSet<LocalUser> LocalUsers { get; set; }

        public static void AddTestData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var localUser = new LocalUser
            {
                Id = 1,
                UserName = "abc@gmail.com",
                Name = "abc",
                Password = "admin123@"
            };
            context.LocalUsers.Add(localUser);

            var localUser1 = new LocalUser
            {
                Id = 2,
                UserName = "xyz@gmail.com",
                Name = "xyz",
                Password = "admin123@"
            };
            context.LocalUsers.Add(localUser1);

            var testVilla1 = new Villa
            {
                Id = 1,
                Name = "Beach View",
                Details="This is a famaus beach",
                Amenity="Have beautiful rooms",
                Sqft=200,
                Rate=400.00,
                Occupancy=4,
                ImageUrl="",
                CreatedDate=new DateTime(),
                UpdatedDate=new DateTime()
            };

            context.Villas.Add(testVilla1);

            var testVilla2 = new Villa
            {
                Id = 2,
                Name = "Pool View",
                Details = "This is a famaus beach",
                Amenity = "Have beautiful rooms",
                Sqft = 150,
                Rate = 200.00,
                Occupancy = 3,
                ImageUrl = "",
                CreatedDate = new DateTime(),
                UpdatedDate = new DateTime()
            };

            context.Villas.Add(testVilla2);

            var testVilla3 = new Villa
            {
                Id = 3,
                Name = "Beach Plain View",
                Details = "This is a famaus beach",
                Amenity = "Have beautiful rooms",
                Sqft = 200,
                Rate = 400.00,
                Occupancy = 4,
                ImageUrl = "",
                CreatedDate = new DateTime(),
                UpdatedDate = new DateTime()
            };

            context.Villas.Add(testVilla3);

            var testVilla4 = new Villa
            {
                Id = 4,
                Name = "Pool plain View",
                Details = "This is a famaus beach",
                Amenity = "Have beautiful rooms",
                Sqft = 150,
                Rate = 200.00,
                Occupancy = 3,
                ImageUrl = "",
                CreatedDate = new DateTime(),
                UpdatedDate = new DateTime()
            };

            context.Villas.Add(testVilla4);

            var testVilla5 = new Villa
            {
                Id = 5,
                Name = "High Beach View",
                Details = "This is a famaus beach",
                Amenity = "Have beautiful rooms",
                Sqft = 200,
                Rate = 400.00,
                Occupancy = 4,
                ImageUrl = "",
                CreatedDate = new DateTime(),
                UpdatedDate = new DateTime()
            };

            context.Villas.Add(testVilla5);

            var testVilla6 = new Villa
            {
                Id = 6,
                Name = "High Pool View",
                Details = "This is a famaus beach",
                Amenity = "Have beautiful rooms",
                Sqft = 150,
                Rate = 200.00,
                Occupancy = 3,
                ImageUrl = "",
                CreatedDate = new DateTime(),
                UpdatedDate = new DateTime()
            };

            context.Villas.Add(testVilla6);

            context.SaveChanges();
        }
    }
}

