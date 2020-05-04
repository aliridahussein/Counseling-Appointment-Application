using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class Broadcast
    {
        public Broadcast()
        {
            Date = DateTime.Now;
        }
        public string BroadcastId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }

        //public string categoryId { get; set; }
    }
}
