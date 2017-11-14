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
    /// Mapping class for Facility Group Member Control Value
    /// </summary>
    public sealed class FacilityGroupMemberControlValueMap : EntityTypeConfiguration<FacilityGroupMemberControlValue>
    {
        public FacilityGroupMemberControlValueMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("FacilityGroupMemberControlValues");
        }
    }
}
