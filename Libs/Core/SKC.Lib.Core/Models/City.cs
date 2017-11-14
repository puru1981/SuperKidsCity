using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// City Entity Class
    /// </summary>
    public class City : Base
    {
        private ICollection<Locality> _localities;

        /// <summary>
        /// Name of the city
        /// </summary>
        public string Name { get; set; }

        //Short code for the city
        public string Code { get; set; }

        /// <summary>
        /// Foreign Key of State Object
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// List of localities in city
        /// </summary>
        public virtual ICollection<Locality> Localities
        {
            get
            {
                return _localities ?? new List<Locality>();
            }
            set
            {
                _localities = value;
            }
        }
    }
}
