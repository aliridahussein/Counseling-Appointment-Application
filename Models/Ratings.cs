using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class Ratings
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CounselorId { get; set; }
        public float Rate { get; set; }

    }
}
