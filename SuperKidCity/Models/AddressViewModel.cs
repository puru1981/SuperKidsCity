using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Models
{
    public class AddressViewModel:BaseViewModel
    {
        public AddressViewModel()
        {
            this.Districts = new List<SelectListItem>(){
                 new SelectListItem(){
                       Text = "--Select--",
                       Selected = true
                 },
            };

            this.Localities = new List<SelectListItem>(){
                 new SelectListItem(){
                       Text = "--Select--",
                       Selected = true
                 },
            };

            this.States = new List<SelectListItem>(){
                 new SelectListItem(){
                       Text = "--Select--",
                       Selected = true
                 },
            };
        }
        [Required]
        [Display(Name="Address Line 1")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name="Address Line 2")]
        public string StreeetAddress2 { get; set; }

        public List<SelectListItem> Localities { get; set; }

        public string Locality { get; set; }

        public List<SelectListItem> Districts { get; set; }

        [Display(Name="City")]
        [Required]
        public string District { get; set; }

        public List<SelectListItem> States { get; set; }

        public string State { get; set; }
    }
}