using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core
{
    /// <summary>
    /// Base Interface
    /// </summary>
   public interface IBase
    {
       /// <summary>
       /// Primary Key 
       /// </summary>
       int Id { get; set; }

       /// <summary>
       /// Date Time for the Last Update
       /// </summary>
       DateTime UpdatedAt { get; set; }

       /// <summary>
       /// Date Time of the Creation
       /// </summary>
       DateTime CreatedAt { get; set; }

       /// <summary>
       /// User Id of the User currently Logged in
       /// </summary>
       string UserId { get; set; }

       /// <summary>
       /// Whether the Record is deleted
       /// </summary>
       bool Deleted { get; set; }

       /// <summary>
       /// Whether the Record is active
       /// </summary>
       bool Active { get; set; }

       /// <summary>
       /// Session Id of the Request
       /// </summary>
       string SessionId { get; set; }
    }
}
