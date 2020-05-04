using E_CounsellingWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public interface IBroadcastRepository
    {
        Broadcast GetBroadCast(int Email);
        IEnumerable<Broadcast> GetAllBroadcasts();
        Broadcast Add(Broadcast broadcast);
        Broadcast Update(Broadcast BroadcastChanges);
        Broadcast Delete(string Email);

    }
}
