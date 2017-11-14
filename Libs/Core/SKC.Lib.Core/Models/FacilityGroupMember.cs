using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Facility Group Member Class
    /// </summary>
   public class FacilityGroupMember:Base
    {
       private ICollection<FacilityGroupMemberControlOption> _options;

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
       /// Type of the Control to be dispalyed for input
       /// </summary>
       public FacilityGroupMemberType Type { get; set; }

       /// <summary>
       /// Facility Group Object
       /// </summary>
       public virtual FacilityGroup Group { get; set; }

       /// <summary>
       /// Collection of Control Options
       /// </summary>
       public virtual ICollection<FacilityGroupMemberControlOption> Options
       {
           get
           {
               return _options ?? new List<FacilityGroupMemberControlOption>();
           }
           set
           {
               _options = value;
           }
       }
    }
}
