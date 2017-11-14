using SKC.Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    /// <summary>
    /// View Model Class for Members
    /// </summary>
    public class MemberViewModel:BaseViewModel
    {
        public MemberViewModel()
        {
            this.Contact = new ContactViewModel();
        }
        /// <summary>
        /// Full Name of the Member
        /// </summary>
        //[Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        /// <summary>
        /// First Name of the Member
        /// </summary>
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the Member
        /// </summary>
        [Required]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gender of the Member
        /// </summary>
        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        /// <summary>
        /// Type of the Member
        /// </summary>
        [Required]
        [Display(Name ="Designation")]
        public MemberType Type { get; set; }

        public ContactViewModel Contact { get; set; }

    }
}