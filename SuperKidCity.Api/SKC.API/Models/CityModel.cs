using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKC.API.Models
{
    public sealed class CityModel : BaseModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int StateId { get; set; }

        public int Localities { get; set; }
    }
}