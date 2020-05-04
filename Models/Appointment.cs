using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class Appointment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CounselorId { get; set; }
        public string UserName { get; set; }
        public string CounselorName { get; set; }
        public bool Accepted { get; set; }
        public bool Appointed { get; set; }
        public DateTime Date { get; set; }
    }
}
