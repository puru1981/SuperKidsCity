using SKC.Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Data.Models.Mappings
{
    /// <summary>
    /// City Map Class
    /// </summary>
    public sealed class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            this.HasKey(k => k.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            this.ToTable("Cities");
        }
    }
}
