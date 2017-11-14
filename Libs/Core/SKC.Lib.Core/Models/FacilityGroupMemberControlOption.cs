using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Class for Facility Group Member Control Options
    /// </summary>
   public class FacilityGroupMemberControlOption:Base
    {
       /// <summary>
       /// Option Value
       /// </summary>
       public string Option { get; set; }

       /// <summary>
       /// Facility Group Member Object
       /// </summary>
       public virtual FacilityGroupMember GroupMember { get; set; }
    }
}
