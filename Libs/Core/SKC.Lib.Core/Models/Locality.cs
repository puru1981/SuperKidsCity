using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Locality Entity Class
    /// </summary>
    public class Locality : Base
    {
        /// <summary>
        /// Name of the locality
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// short code for the locality
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// district for the locality
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Foreign Key of State Object
        /// </summary>
        public int CityId { get; set; }
    }
}
