using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Member Entity Class
    /// </summary>
    public class Member : Base
    {
        //private ICollection<Address> _addresses;
        /// <summary>
        /// First Name of the Member
        /// </summary>
        public string FirtsName { get; set; }

        /// <summary>
        /// Last Name of the Member
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gender of the Member
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Type of the Member
        /// </summary>
        public MemberType Type { get; set; }

        /// <summary>
        /// School to which the member belong
        /// </summary>
        public virtual School School { get; set; }

    }
}
