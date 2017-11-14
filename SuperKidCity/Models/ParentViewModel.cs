using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Models
{
    public class ParentViewModel
    {
        public ParentViewModel()
        {
            this.Member = new MemberViewModel();
            this.Schools = new List<SelectListItem>();
        }

        [UIHint("MemberView")]
        public MemberViewModel Member { get; set; }

        [Required]
        [Display(Name="Registration No (Student)")]
        public string RegistrationNumber { get; set; }

        public List<SelectListItem> Schools { get; set; }

        [Required]
        [Display(Name="School")]
        public string School { get; set; }

        public int SchoolId { get; set; }
    }
}