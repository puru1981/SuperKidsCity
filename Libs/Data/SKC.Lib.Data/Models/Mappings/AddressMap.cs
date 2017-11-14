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
    /// Mapping Class for Addresses
    /// </summary>
    public sealed class AddressMap : EntityTypeConfiguration<Address>
    {
        #region Ctor

        public AddressMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("Addresses");
            //this.HasRequired(a => a.Locality);
        } 
        #endregion
    }
}
