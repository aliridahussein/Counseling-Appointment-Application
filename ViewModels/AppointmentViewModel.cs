using E_CounsellingWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.ViewModels
{
    public class AppointmentViewModel
    {
        public List<Appointment> MyAppointments { get; set; }
        public List<Appointment> RequestedAppointments { get; set; }
        public List<Appointment> AvailableAppointments { get; set; }
    }
}
