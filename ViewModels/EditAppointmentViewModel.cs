using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.ViewModels
{
    public class EditAppointmentViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CounselorId { get; set; }
        public DateTime Date { get; set; }
    }
}
