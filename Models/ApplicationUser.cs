using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Hubs;

namespace E_CounsellingWebApplication.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
            Messages = new HashSet<Message>();
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public string PhotoPath { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public float Rate { get; set; }
        public string Category { get; set; }


    }
}
