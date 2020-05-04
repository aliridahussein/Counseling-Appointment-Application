using E_CounsellingWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class SqlAppointmentRepository:IAppointmentRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public SqlAppointmentRepository(AppDbContext context,
                                          UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public Appointment Add(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
            return appointment;
        }

        public IEnumerable<Appointment> GetAllUserAppointments()
        {
            return context.Appointments;
        }

        public void Delete(Appointment appointment)
        {
           context.Appointments.Remove(appointment);
           context.SaveChanges();
        }
        public bool exist( string counselorId,DateTime date)
        {
            var mylist = context.Appointments;
            foreach (var item in mylist)
            {
                if (item.CounselorId == counselorId && item.Date == date)
                {
                    return true;

                }
            }

            return false;

        }
        public Appointment Update(Appointment AppointmentChanges)
        {
            var Appointment = context.Attach(AppointmentChanges);
            Appointment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return AppointmentChanges;
        }

        public Appointment GetAppointment(string Id)
        {
            return context.Appointments.Find(Id);

        }

    }
}
