using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKC.API.Models
{
    public sealed class LocalityModel:BaseModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int CityId { get; set; }
    }
}