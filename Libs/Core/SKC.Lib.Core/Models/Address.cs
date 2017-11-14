using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Address Entity Class
    /// </summary>
    public class Address : Base
    {
        private ICollection<Contact> _contactNumbers;

        private ICollection<Member> _persons;
        /// <summary>
        /// Address Line 1 , i.e House Address
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Address Line 2, i.e Street Address
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Locality Id Foreign Key
        /// </summary>
        public int Locality_Id { get; set; }

        /// <summary>
        /// School Object
        /// </summary>
        public virtual School School { get; set; }

        /// <summary>
        /// Collection of Members for the particular address 
        /// </summary>
        public virtual ICollection<Member> Persons
        {
            get
            {
                return _persons ?? new List<Member>();
            }
            set { _persons = value; }
        }

        /// <summary>
        /// Collection of Contact Numbers for the particular address 
        /// </summary>
        public virtual ICollection<Contact> ContactNumbers
        {
            get { return _contactNumbers ?? new List<Contact>(); }
            set { _contactNumbers = value; }
        }
    }
}
