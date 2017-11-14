using SKC.Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Data.Models.Mappings
{
    /// <summary>
    /// Mapping Class for Contacts
    /// </summary>
    public sealed class ContactMap : EntityTypeConfiguration<Contact>
    {
        #region Ctor

        public ContactMap()
        {
            this.HasKey(k => k.Id);
           // this.HasRequired<Address>(p => p.Address).WithMany(p => p.ContactNumbers).HasForeignKey(p=>p.AddressId);
            this.ToTable("Contacts");
        } 

        #endregion
    }
}
