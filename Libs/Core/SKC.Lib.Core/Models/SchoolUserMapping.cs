using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Mapping Class for User-School
    /// </summary>
   public class SchoolUserMapping:Base
    {
       /// <summary>
       /// School Virtual Object
       /// </summary>
       public virtual School School { get; set; }

       /// <summary>
       /// User Id
       /// </summary>
       public string UserId { get; set; }
    }
}
