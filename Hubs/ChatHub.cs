using E_CounsellingWebApplication.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace E_CounsellingWebApplication.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext context;

        public ChatHub(AppDbContext context)
        {
            this.context = context;
        }
        
        public void SendMessage(string foruser, string user, string message)
        {
            Clients.User(foruser).SendAsync("ReceiveMessage", user, message);
            var connection = new ChatConnection()
            {
                User = user

            };
            context.Connections.Add(connection);
            context.SaveChanges();
        }




    }
}