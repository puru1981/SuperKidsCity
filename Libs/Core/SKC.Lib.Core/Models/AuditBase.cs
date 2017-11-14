using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Log Class for Logging the system logs
    /// </summary>
    public class AuditBase : IAuditBase, IBase
    {
        /// <summary>
        /// IPv4 of the client
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Platform of the client machine
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Custome Message if any
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Whether the requested recource threw any error
        /// </summary>
        public bool HasError { get; set; }

       /// <summary>
       /// Resuested resource 
       /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date Time of Last Update
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Date Time of creation
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// User Id of the Logged in User
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Whether the record is soft deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Whether the record is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Session Id of the request
        /// </summary>
        public string SessionId { get; set; }
    }
}
