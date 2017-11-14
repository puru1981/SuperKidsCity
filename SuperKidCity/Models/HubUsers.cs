using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public sealed class HubUsers : BaseViewModel
    {
        public HubUsers()
        {
            this.Active = true;
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }

        public string IPAddress { get; set; }

        public string ConnectionId { get; set; }

        public string UserName { get; set; }

        public string Url { get; set; }

        public bool IsCaller { get; set; }
    }
}