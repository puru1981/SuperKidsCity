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
    /// Mappping class for Facility Group Member Control Option
    /// </summary>
    public sealed class FacilityGroupMemberControlOptionMap : EntityTypeConfiguration<FacilityGroupMemberControlOption>
    {
        public FacilityGroupMemberControlOptionMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("FacilityGroupMemberControlOptions");
        }
    }
}
