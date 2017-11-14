using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class CamSettingViewModel:BaseViewModel
    {
        public string Name { get; set; }

        [Display(Name="IPv4 Address")]
        public string IPv4 { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [Display(Name="Authentication Required")]
        public bool AuthenticationRequired { get; set; }
    }
}