
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core
{
    /// <summary>
    /// Audit Base Interface
    /// </summary>
    /// <typeparam name="T"> IBase </typeparam>
   public interface IAuditBase
    {
       /// <summary>
       /// IP Address of the Client
       /// </summary>
       string IP { get; set; }

       /// <summary>
       /// Platform of the Client
       /// </summary>
       string Platform { get; set; }
       
       /// <summary>
       /// Custom Text Message for the Log
       /// </summary>
       string Message { get; set; }

       /// <summary>
       /// Whether the Requested Entity throws any error
       /// </summary>
       bool HasError { get; set; }

       /// <summary>
       /// Requested Entity Name
       /// </summary>
       string Entity { get; set; }
    }
}
