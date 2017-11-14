using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKC.API.Models
{
    public sealed class StateModel:BaseModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int Cities { get; set; }
    }
}