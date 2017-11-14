using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// School Entity Class
    /// </summary>
   public class School:Base
    {
       /// <summary>
       /// Name of the School
       /// </summary>
       public string Name { get; set; }

       /// <summary>
       /// Registration Number
       /// </summary>
       public string RegistrationNumber { get; set; }

       /// <summary>
       /// Date Time of the establishment
       /// </summary>
       public DateTime EstablishedAt { get; set; }

       /// <summary>
       /// URL of the School Website
       /// </summary>
       public string WebUrl { get; set; }

       /// <summary>
       /// Short Description about the school
       /// </summary>
       public string ShortBio { get; set; }

       /// <summary>
       /// Address Object
       /// </summary>
      // public virtual Address Address { get; set; }
    }
}
