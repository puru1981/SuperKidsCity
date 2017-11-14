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
    /// Mapping Class for Members
    /// </summary>
    public sealed class MemberMap : EntityTypeConfiguration<Member>
    {
        #region Ctor

        public MemberMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("Members");
        } 

        #endregion
    }
}
