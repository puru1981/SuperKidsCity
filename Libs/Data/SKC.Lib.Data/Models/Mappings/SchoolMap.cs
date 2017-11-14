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
    /// Mapping Class for Schools
    /// </summary>
    public sealed class SchoolMap : EntityTypeConfiguration<School>
    {
        #region Ctor

        public SchoolMap()
        {
            this.HasKey(k => k.Id);
            //this.HasRequired(p => p.Address).WithRequiredPrincipal(p=> p.School);
            this.ToTable("Schools");
        } 

        #endregion
    }
}
