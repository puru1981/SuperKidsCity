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
    /// Locality Map Class
    /// </summary>
    public sealed class LocalityMap : EntityTypeConfiguration<Locality>
    {
        public LocalityMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("Localities");
        }
    }
}
