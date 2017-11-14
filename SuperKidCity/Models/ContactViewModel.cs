using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class ContactViewModel:BaseViewModel
    {
        [Display(Name=" Email")]
        public string Email { get; set; }

        [Display(Name="Contact Number")]
        [Phone]
        public string Number { get; set; }
    }
}