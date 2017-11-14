using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class RFIDSettingsViewModel : BaseViewModel
    {
        [Display(Name = "Reader Name")]
        public string ReaderName { get; set; }

        [Display(Name = "Reader Id")]
        public string ReaderId { get; set; }

        [Display(Name = "Data Format")]
        public string DataFormat { get; set; }

        [Display(Name = "IPv4 Address")]
        public string IPv4 { get; set; }

        public int Port { get; set; }

        [Display(Name = "Authentication Required")]
        public bool AuthenticationRequired { get; set; }
    }
}