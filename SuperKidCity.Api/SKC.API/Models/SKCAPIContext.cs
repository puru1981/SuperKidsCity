using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SKC.API.Models
{
    public class SKCAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SKCAPIContext() : base("name=SKCAPIContext")
        {
        }

        public System.Data.Entity.DbSet<SKC.API.Models.StateModel> StateModels { get; set; }

        public System.Data.Entity.DbSet<SKC.API.Models.CityModel> CityModels { get; set; }

        public System.Data.Entity.DbSet<SKC.API.Models.LocalityModel> LocalityModels { get; set; }
    
    }
}
