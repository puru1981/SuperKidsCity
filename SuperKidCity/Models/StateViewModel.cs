using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class StateViewModel
    {
        public StateViewModel()
        {
            this.Cities = new List<CityViewModel>();
        }
        public string Name { get; set; }

        public string Code { get; set; }

        public int Id { get; set; }

        public IList<CityViewModel> Cities { get; set; }
    }
}