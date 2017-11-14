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
    /// Mapping Class for Facility Groups
    /// </summary>
    public sealed class FacilityGroupMap : EntityTypeConfiguration<FacilityGroup>
    {
        #region Ctor

        public FacilityGroupMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("FacilityGroups");
        } 

        #endregion
    }
}
