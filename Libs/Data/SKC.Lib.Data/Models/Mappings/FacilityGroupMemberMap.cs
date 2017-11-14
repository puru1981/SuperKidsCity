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
    /// Mapping Class for Facility Group Members
    /// </summary>
    public sealed class FacilityGroupMemberMap : EntityTypeConfiguration<FacilityGroupMember>
    {
        #region Ctor
        
        public FacilityGroupMemberMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("FacilityGroupMembers");
        } 

        #endregion
    }
}
