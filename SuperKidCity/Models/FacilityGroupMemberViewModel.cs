using SKC.Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperKidCity.Models
{
    /// <summary>
    /// View Model for Facility Group Member
    /// </summary>
    public class FacilityGroupMemberViewModel:BaseViewModel
    {
        /// <summary>
        /// School Id
        /// </summary>
        public int SchoolId { get; set; }

        /// <summary>
        /// Facility Group Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Facility Group Member Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of value to be accepted
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// Unique Id
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// Whether applicable for input
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Comma Separated Values for drop down controls
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// Type of the Control to be dispalyed for input
        /// </summary>
        public FacilityGroupMemberType Type { get; set; }
    }
}
