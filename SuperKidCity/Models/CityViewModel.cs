using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class CityViewModel
    {
        public CityViewModel()
        {
            this.Localities = new List<LocalityViewModel>();
        }
        public string Name { get; set; }

        public string Code { get; set; }

        public string State { get; set; }

        public int Id { get; set; }

        public IList<LocalityViewModel> Localities { get; set; }
    }
}