using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SuperKidCity.Models
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal):base(principal)
        {
            this.IsAuthenticated = principal.Identity.IsAuthenticated;
            this.Name = principal.Identity.Name;
        }
        public bool IsAuthenticated { get; protected set; }

        public string Name { get; protected set; }

        public string UserId
        {
            get
            {
                if (this.IsAuthenticated)
                {
                    return this.FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                else
                    return string.Empty;
            }
        }

        public string UserFullName
        {
            get
            {
                if (this.IsAuthenticated)
                    return this.FindFirst(ClaimTypes.GivenName).Value;
                else
                    return string.Empty;
            }
        }

        public string Email
        {
            get
            {
                if (this.IsAuthenticated)
                    return this.FindFirst(ClaimTypes.Email).Value;
                else
                    return string.Empty;
            }
        }

        public string LoggedSince
        {

            get
            {
                if (this.IsAuthenticated)
                    return this.FindFirst(ClaimTypes.SerialNumber).Value;
                else
                    return string.Empty;
            }

        }

        public string SerialId
        {
            get
            {
                if (this.IsAuthenticated)
                    return this.FindFirst(ClaimTypes.Sid).Value;
                else
                    return string.Empty;
            }
        }
    }
}