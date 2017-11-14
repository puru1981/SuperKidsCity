using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SuperKidCity.Models;

namespace SuperKidCity
{

    public class LiveStreamHub : Hub
    {
        private static Dictionary<string, HubUsers> clients;

        public void Greetings()
        {
            var username = Context.QueryString["username"];
            var url = Context.QueryString["currentUrl"];
            var hostAddress = Context.QueryString["clientIp"];

            string greetingMsg = string.Format("Welcome, {0} on {1}", username, hostAddress);
            Clients.Caller.setContext(Context.ConnectionId, greetingMsg);
        }

        public void PostData(string imgData)
        {
            Clients.AllExcept(new string []{Context.ConnectionId}).showLive(imgData);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            var username = Context.QueryString["username"];
            var url = Context.QueryString["currentUrl"];
            var hostAddress = Context.QueryString["clientIp"];

            if (clients == null)
            {
                clients = new Dictionary<string, HubUsers>();
            }
            clients.Add(Context.ConnectionId, new HubUsers() { ConnectionId = Context.ConnectionId, IPAddress = hostAddress, UserName = username, Url = url });
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            if (clients != null && clients.Count > 0)
            {
                clients.Remove(Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            var username = Context.QueryString["username"];
            var url = Context.QueryString["currentUrl"];
            var hostAddress = Context.QueryString["clientIp"];

            if (clients != null && clients.Count > 0)
            {
                clients.Add(Context.ConnectionId, new HubUsers() { ConnectionId = Context.ConnectionId, IPAddress = hostAddress, UserName = username, Url = url });
            }
            return base.OnReconnected();
        }
    }
}