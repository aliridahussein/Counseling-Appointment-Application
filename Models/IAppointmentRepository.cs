using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public interface IAppointmentRepository
    {
        Appointment Add(Appointment appointment);
        IEnumerable<Appointment> GetAllUserAppointments();
        void Delete(Appointment appointment);
        bool exist(string counselorId, DateTime date);
        Appointment Update(Appointment AppointmentChanges);
        Appointment GetAppointment(string Id);


    }
}
