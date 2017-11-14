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
    /// State Map Class
    /// </summary>
    public sealed class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("States");
        }
    }
}
