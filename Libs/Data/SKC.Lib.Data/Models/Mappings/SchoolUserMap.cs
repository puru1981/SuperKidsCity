using SKC.Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Data.Models.Mappings
{
   public class SchoolUserMap:EntityTypeConfiguration<SchoolUserMapping>
    {
       public SchoolUserMap()
       {
           this.HasKey(k => k.Id);
           this.ToTable("SchoolUsers");
       }
    }

}
