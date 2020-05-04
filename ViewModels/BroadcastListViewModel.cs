using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.ViewModels
{
    public class BroadcastListViewModel
    {
        public string BroadcastId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public string UserId { get; set; }
        public string PhotoPath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }

    }
}
