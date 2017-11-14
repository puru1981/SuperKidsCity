namespace SuperKidCity.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SKC.Lib.Core.Models;
    using SKC.Lib.Data.Models.Entities;
    using SuperKidCity.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<SuperKidCity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SuperKidCity.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            SeedDatabase(context);
        }

        private static void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }

        private static async void SeedDatabase(SuperKidCity.Models.ApplicationDbContext context)
        {
            if (context.Roles.ToList().Count == 0)
            {
                context.Roles.AddOrUpdate(
                    new IdentityRole { Name = "Sa" },
                    new IdentityRole { Name = "Admin" },
                    new IdentityRole { Name = "School" },
                    new IdentityRole { Name = "Parent" }
                    );
            }

            if (context.Users.ToList().Count == 0)
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new ApplicationUserManager(store);
                var admin = new ApplicationUser() { Email = "admin@skc.com", UserName = "admin@skc.com" };
                var sa = new ApplicationUser() { Email = "sa@skc.com", UserName = "sa@skc.com" };
                await manager.CreateAsync(admin, "Admin@12345");
                await manager.CreateAsync(sa, "Admin@12345");
                await manager.AddToRoleAsync(admin.Id, "Admin");
                await manager.AddToRoleAsync(sa.Id, "Sa");
            }

            var sessionId = Guid.NewGuid().ToString();
            var userId = "0";


            

            if (context.States.ToList().Count == 0)
            {
                var states = new List<StateDTO>();
                states.Add(new StateDTO
                {
                    Name = "ANDAMAN & NIKOBAR ISLANDS",
                    Code = "ANI",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "ANDHRA PRADESH",
                    Code = "AP",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "ARUNACHAL PRADESH",
                    Code = "ARP",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "ASSAM",
                    Code = "AS",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "BIHAR",
                    Code = "BR",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "CHANDIGARH",
                    Code = "CHN",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "CHHATTISGARH",
                    Code = "CH",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "DADRA & NAGAR HAVELI",
                    Code = "DNH",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "DAMAN & DIU",
                    Code = "DD",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "GOA",
                    Code = "GA",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "GUJARAT",
                    Code = "GJ",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "HARYANA",
                    Code = "HR",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "HIMACHAL PRADESH",
                    Code = "HP",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "JAMMU & KASHMIR",
                    Code = "JK",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "JHARKHAND",
                    Code = "JH",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "KARNATAKA",
                    Code = "KR",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "KERALA",
                    Code = "KR",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "LAKSHADWEEP",
                    Code = "LKD",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "MADHYA PRADESH",
                    Code = "MP",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "MAHARASHTRA",
                    Code = "MH",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "MANIPUR",
                    Code = "MN",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "MEGHALAYA",
                    Code = "MG",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "MIZORAM",
                    Code = "MZ",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "NAGALAND",
                    Code = "NG",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "NCT OF DELHI",
                    Code = "DLNCR",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "ORISSA",
                    Code = "OR",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "PUDUCHERRY",
                    Code = "PD",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "PUNJAB",
                    Code = "PB",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "RAJASTHAN",
                    Code = "RJ",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "SIKKIM",
                    Code = "SK",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "TAMIL NADU",
                    Code = "TN",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "TRIPURA",
                    Code = "TR",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "UTTAR PRADESH",
                    Code = "UP",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "UTTARAKHAND",
                    Code = "UK",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
                states.Add(new StateDTO
                {
                    Name = "WEST BENGAL",
                    Code = "WB",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    SessionId = sessionId,
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                });
            }
            SaveChanges(context);
        }
    }
}
