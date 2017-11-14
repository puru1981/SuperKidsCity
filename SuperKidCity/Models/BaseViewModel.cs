using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Active { get; set; }
    }
}