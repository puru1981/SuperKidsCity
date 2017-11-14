using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SuperKidCity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(SKC.Lib.Data.Models.Mappings.BaseLogMap).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SKC.Lib.Data.Models.Entities.BaseLog> Logs { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.SchoolDTO> Schools { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.AddressDTO> Addresses { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.ContactDTO> Contacts { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.MemberDTO> Members { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.FacilityGroupDTO> FacilityGroups { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.FacilityGroupMemberDTO> FacilityGroupMembers { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.SchoolUserDTO> SchoolUsers { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.FacilityGroupMemberControlOptionDTO> FacilityGroupMemberControlOptions { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.FacilityGroupMemberControlValueDTO> FacilityGroupMemberControlValues { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.ParentDTO> Parents { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.LocalityDTO> Localities { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.CityDTO> Cities { get; set; }

        public DbSet<SKC.Lib.Data.Models.Entities.StateDTO> States { get; set; }
    }
}