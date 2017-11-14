using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    /// <summary>
    /// View Model Class for School
    /// </summary>
    public class SchoolViewModel:BaseViewModel
    {
        public SchoolViewModel()
        {
            this.ContactPerson = new MemberViewModel();
            this.ContactPerson.Type = SKC.Lib.Core.Models.MemberType.ContactPerson;
            this.Principal = new MemberViewModel();
            this.Principal.Type = SKC.Lib.Core.Models.MemberType.Principal;
            this.Address = new AddressViewModel();
            this.Facilities = new List<FacilityGroupMemberViewModel>();
        }

        /// <summary>
        /// Name of the School
        /// </summary>
        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }

        /// <summary>
        /// Registration Number
        /// </summary>
        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Date Time of the establishment
        /// </summary>
        [Required]
        [Display(Name = "Establishment: (MM-YY)")]
        public DateTime EstablishedAt { get; set; }

        /// <summary>
        /// Short description of the school
        /// </summary>
        [Display(Name = "Short Description")]
        [MaxLength(250, ErrorMessage = "Too Long Description. Only 250 characters are allowed")]
        public string ShortBio { get; set; }

        /// <summary>
        /// Website URL of the school
        /// </summary>
        [Display(Name = "Website")]
        public string Url { get; set; }

        /// <summary>
        /// Contact Person Member Object
        /// </summary>
        [UIHint("MemberView")]
        public MemberViewModel ContactPerson { get; set; }

        /// <summary>
        /// Principal Member Object
        /// </summary>
        [UIHint("MemberView")]
        public MemberViewModel Principal { get; set; }

        /// <summary>
        /// Address Object
        /// </summary>
        public AddressViewModel Address { get; set; }

        public IList<FacilityGroupMemberViewModel> Facilities { get; set; }
    }
}