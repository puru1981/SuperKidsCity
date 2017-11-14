using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Class for Parent
    /// </summary>
   public class Parent:Base
    {
       /// <summary>
       /// Registartion Number of the Member's Child
       /// </summary>
       public string StudentRegdNo { get; set; }

       /// <summary>
       /// Detail of the Member
       /// </summary>
       public virtual Member Member { get; set; }

       /// <summary>
       /// School of the Member's child
       /// </summary>
       public virtual School School { get; set; }
    }
}
