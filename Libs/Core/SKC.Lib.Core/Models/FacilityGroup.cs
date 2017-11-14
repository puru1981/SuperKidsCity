using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Facility Group Class
    /// </summary>
    public class FacilityGroup : Base
    {
        private ICollection<FacilityGroupMember> _members;

        /// <summary>
        /// Name of the Group
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Collection of associated members
        /// </summary>
        public ICollection<FacilityGroupMember> Members
        {
            get
            {
                return _members ?? new List<FacilityGroupMember>();
            }
            set
            {
                _members = value;
            }
        }
    }
}
