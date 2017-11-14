using SKC.Lib.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Service
{
    public sealed class RespositoryContext : DbContext
    {
        public RespositoryContext(string connStr)
            : base("SKC")
        {
            this.Database.Connection.ConnectionString = connStr;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(SKC.Lib.Data.Models.Mappings.BaseLogMap).Assembly);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }

        protected override bool ShouldValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        {
            return base.ShouldValidateEntity(entityEntry);
        }

        public DbSet<StateDTO> States { get; set; }

        //public DbSet<AddressDTO> Addresses { get; set; }

        //public DbSet<LocalityDTO> Localities { get; set; }

        //public DbSet<MemberDTO> Members { get; set; }

        //public DbSet<ContactDTO> Contacts { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
