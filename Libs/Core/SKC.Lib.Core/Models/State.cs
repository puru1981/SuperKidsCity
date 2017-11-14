using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// State Entity Class
    /// </summary>
    public class State : Base
    {
        private ICollection<City> _cities;

        /// <summary>
        /// Name of the state
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// short code for the state
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// List of cities in the state
        /// </summary>
        public virtual ICollection<City> Cities
        {
            get
            {
                return _cities ?? new List<City>();
            }
            set
            {
                _cities = value;
            }
        }
    }
}
