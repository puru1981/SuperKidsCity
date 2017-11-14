using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models.IndentityServer
{
    /// <summary>
    /// Identity Server Client List
    /// </summary>
    public static class Clients
    {
        /// <summary>
        /// Get all the registered clients
        /// </summary>
        /// <returns> List of registered clients</returns>
        public static IEnumerable<Client> Get()
        {
            return new[]{
                new Client
                {
                     Enabled=true,
                     ClientName="SuperKidCity",
                     ClientId="SKC",
                     Flow = Flows.Implicit,
                     RedirectUris = new List<string>(){
                         "http://localhost:49681/",
                         "https://localhost:44300/"
                     },
                     AllowAccessToAllScopes= true // Just for Sample Application Use, for Production needs to be locked down
                }
            };
        }

    }
}