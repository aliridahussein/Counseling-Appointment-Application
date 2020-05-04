using E_CounsellingWebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace E_CounsellingWebApplication.Models
{
    public class SqlBroadcastRepository: IBroadcastRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public SqlBroadcastRepository(AppDbContext context,
                                      UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public Broadcast Add(Broadcast broadcast)
        {
            context.Broadcasts.Add(broadcast);
            context.SaveChanges();
            return broadcast;
        }

        public Broadcast Delete(string Email)
        {
            Broadcast broadcast = context.Broadcasts.Find(Email);
            if (broadcast != null)
            {
                context.Broadcasts.Remove(broadcast);
                context.SaveChanges();
            }
            return broadcast;
           
        }
        public IEnumerable<Broadcast> GetAllBroadcasts()
        {
            return context.Broadcasts;
        }

        public Broadcast GetBroadCast(int Email )
        {
            return context.Broadcasts.Find(Email);

        }

        public Broadcast Update(Broadcast BroadcastChanges)
        {
            var broadcast = context.Attach(BroadcastChanges);
            broadcast.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return BroadcastChanges;
        }

        public async Task<List<Broadcast>> sortBy(List<Broadcast>allBroadcasts,string sortBy,string sort)
        {
            var sortedList = new List<Broadcast>();
            if (sortBy == "role")
            {

                foreach (var broadcast in allBroadcasts)
                {
                    var user = await userManager.FindByIdAsync(broadcast.UserId);
                    var userRole = await userManager.IsInRoleAsync(user, sort);
                    if (userRole)
                    {
                        sortedList.Add(broadcast);
                    }
                }
            }
            return (sortedList);
        }




    }
}
