using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Base Class for the Entities
    /// </summary>
   public abstract class Base:IBase
    {

       /// <summary>
       /// Primary Key of the Entity
       /// </summary>
       public int Id
       {
           get;
           set;
       }

       /// <summary>
       /// Date Time of Latest Update
       /// </summary>
        public DateTime UpdatedAt
        {
            get;
            set;
        }

       /// <summary>
       /// Date Time of Latest Creation
       /// </summary>
        public DateTime CreatedAt
        {
            get;
            set;
        }

       /// <summary>
       /// User Id of the Currently Logged In User
       /// </summary>
        public string UserId
        {
            get;
            set;
        }

       /// <summary>
       /// Whether the Entity Record is deleted
       /// </summary>
        public bool Deleted
        {
            get;
            set;
        }

       /// <summary>
       /// Whether the Entity Record is active
       /// </summary>
        public bool Active
        {
            get;
            set;
        }

       /// <summary>
       /// Seesion Id of the Request
       /// </summary>
        public string SessionId
        {
            get;
            set;
        }
    }
}
