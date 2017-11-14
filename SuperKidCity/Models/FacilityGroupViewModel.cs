using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    /// <summary>
    /// View Model for Facility Group
    /// </summary>
    public class FacilityGroupViewModel:BaseViewModel
    {
        public FacilityGroupViewModel()
        {
            this.Members = new List<FacilityGroupMemberViewModel>();
        }

        /// <summary>
        /// Group Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// User Defined Name of the group
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Collection of Facility Group Members
        /// </summary>
        [UIHint("FacilityGroupMemberViewModel")]
        public List<FacilityGroupMemberViewModel> Members { get; set; }

    }
}