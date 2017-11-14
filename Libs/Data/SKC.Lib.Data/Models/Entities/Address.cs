using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKC.Lib.Core;
using SKC.Lib.Core.Models;

namespace SKC.Lib.Data.Models.Entities
{
    /// <summary>
    /// DTO for Address
    /// </summary>
    public sealed class AddressDTO : SKC.Lib.Core.Models.Address
    {
        public override ICollection<Contact> ContactNumbers
        {
            get
            {
                return base.ContactNumbers;
            }
            set
            {
                base.ContactNumbers = value;
            }
        }

        public override ICollection<Member> Persons
        {
            get
            {
                return base.Persons;
            }
            set
            {
                base.Persons = value;
            }
        }

    }
}
