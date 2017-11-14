using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Models
{
    public class SearchViewModel : BaseViewModel
    {
        public SearchViewModel()
        {
            this.States = new List<SelectListItem>();
        }
        [Display(Name = "Locality")]
        public string LocalityId { get; set; }

        [Display(Name = "School")]
        public string SchoolId { get; set; }

        [Display(Name = "State")]
        public string StateId { get; set; }

        public List<SelectListItem> States { get; set; }

        public List<FacilityGroupViewModel> Facilities { get; set; }
    }
}