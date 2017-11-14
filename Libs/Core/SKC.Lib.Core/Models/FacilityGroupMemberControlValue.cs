using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Class for Facility Group Member Control Value
    /// </summary>
   public class FacilityGroupMemberControlValue:Base
    {
       /// <summary>
       /// Value of the control
       /// </summary>
       public string Value { get; set; }

       /// <summary>
       /// Data Type of the control
       /// </summary>
       public string DataType { get; set; }

       /// <summary>
       /// School Id
       /// </summary>
       public int SchoolId { get; set; }

       /// <summary>
       /// Control Object
       /// </summary>
       public virtual FacilityGroupMember Control { get; set; }
    }
}
