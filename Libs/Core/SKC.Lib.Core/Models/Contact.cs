using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Contact Entity for Member
    /// </summary>
    public class Contact : Base
    {
        /// <summary>
        /// Phone Number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// School Object
        /// </summary>
        public virtual School School { get; set; }

        /// <summary>
        /// Type of Person to whom the Contact belongs
        /// </summary>
        public MemberType Type { get; set; }

    }
}
