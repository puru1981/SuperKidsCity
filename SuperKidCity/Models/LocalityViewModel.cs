using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class LocalityViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public int Id { get; set; }

        public int CityId { get; set; }
    }
}