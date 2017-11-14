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
    /// Mapping Class for Parent
    /// </summary>
    public sealed class ParentMap : EntityTypeConfiguration<Parent>
    {
        public ParentMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("Parents");
            this.HasRequired(k => k.Member);
            this.HasRequired(k => k.School);
        }
    }
}
