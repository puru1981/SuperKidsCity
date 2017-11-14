using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKC.API.Models
{
    public class BaseModel
    {
        public DateTime CreatedAtUTC { get; set; }

        public DateTime UpdatedAtUTC { get; set; }

        public int Id { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public string Entity { get { return this.GetType().UnderlyingSystemType.Name; } }
    }
}