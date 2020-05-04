using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Models;
using E_CounsellingWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_CounsellingWebApplication.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext context;

        public IUserCounselorRepository ICounselorRepository { get; }

        public AppointmentController(UserManager<ApplicationUser> userManager,
                                     IAppointmentRepository appointmentRepository,
                                     IUserCounselorRepository ICounselorRepository,
                                      SignInManager<ApplicationUser> signInManager,
                                      RoleManager<IdentityRole> roleManager,
                                       AppDbContext context)
        {
            this.userManager = userManager;
            this.appointmentRepository = appointmentRepository;
            this.ICounselorRepository = ICounselorRepository;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }
  
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Email = User.Identity.Name;
            var currentUSer = await userManager.FindByEmailAsync(Email);
            var list = appointmentRepository.GetAllUserAppointments();
            var mylist = new List<Appointment>();
            var availablelist=new List<Appointment>();
            var requestedlist = new List<Appointment>();
            ViewBag.UserId = currentUSer.Id;
            var Counselorslist = ICounselorRepository.GetAllUserCounselors();
            var MyCounselors = new List<ApplicationUser>();
            foreach (var item in Counselorslist)
            {

                if (item.UserID == currentUSer.Id)
                {
                    var counselor = await userManager.FindByIdAsync(item.CounselorId);
                    MyCounselors.Add(counselor);
                };
            }
            

           
            foreach (var item in list)
            {
                if ((item.CounselorId== currentUSer.Id || item.UserId == currentUSer.Id) && item.Appointed==true && item.Accepted==true)
                {
                    if (item.Date >= DateTime.Now)
                    {
                        mylist.Add(item);
                    }
                    else
                    {
                        //appointmentRepository.Delete(item);
                        context.Remove(item);

                    }
                };
            }
            context.SaveChanges();
            if (User.IsInRole("User"))
            {

            
            foreach (var Counselor in MyCounselors)
            {
                foreach (var item in list)
            {
                if ((item.CounselorId == Counselor.Id ) && item.Appointed == false && item.Accepted == true)
                {
                    if (item.Date>=DateTime.Now)
                    {
                        availablelist.Add(item);
                    }
                    else
                    {
                        //appointmentRepository.Delete(item);
                        context.Remove(item);

                    }
                   
                };
                
            }
            }
            context.SaveChanges();
            }
            else
            {
                
               
                    foreach (var item in list)
                    {
                        if ((item.CounselorId == currentUSer.Id) && item.Appointed == false && item.Accepted == true)
                        {
                            if (item.Date >= DateTime.Now)
                            {
                                availablelist.Add(item);
                            }
                            else
                            {
                                //appointmentRepository.Delete(item);
                                context.Remove(item);

                            }

                        };

                    }
                
                context.SaveChanges();
            }

            foreach (var item in list)
            {
                if ((item.CounselorId == currentUSer.Id || item.UserId == currentUSer.Id) && item.Appointed == false && item.Accepted == false)
                {
                    if (item.Date >= DateTime.Now)
                    {
                        requestedlist.Add(item);
                    }
                    else
                    {
                        //appointmentRepository.Delete(item);
                        context.Remove(item);

                    }

                };
            }
            context.SaveChanges();
            availablelist.OrderBy(s => s.Date);
            mylist.OrderBy(s => s.Date);

            var appointmentviewmodel = new AppointmentViewModel()
            {
                MyAppointments=mylist,
                AvailableAppointments=availablelist,
                RequestedAppointments=requestedlist
            };
                return View(appointmentviewmodel);
        }
        public IActionResult RequestAppointment()
        {
            return View();
        }

       
        public async Task<IActionResult> Appoint(string Id)
        {
            var counselorEmail = User.Identity.Name;
            var user = await userManager.FindByEmailAsync(counselorEmail);
            var curretnAppointment = appointmentRepository.GetAppointment(Id);
            curretnAppointment.Appointed = true;
            curretnAppointment.UserId = user.Id;
            curretnAppointment.UserName = user.FirstName + " " + user.LastName;
            appointmentRepository.Update(curretnAppointment);
            
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> AddAvailableAppointments(string CounselorEmail)
        {
            var user = await userManager.FindByEmailAsync(CounselorEmail);
            var model = new AppointViewModel()
            {
               CounselorId= user.Id
            };
            return View("Appoint",model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAvailableAppointments(AppointViewModel appointViewModel)
        {
            var counselorEmail = User.Identity.Name;
            var user = await userManager.FindByEmailAsync(counselorEmail);
            var appointment = new Appointment()
            {
                CounselorId = user.Id,
                Accepted = true,
                Appointed = false,
                Date = appointViewModel.Date,
                CounselorName=user.FirstName+" "+user.LastName
                
            };
            appointmentRepository.Add(appointment);
            return RedirectToAction("Index");
        }

        public IActionResult CancleAppointment(string AppointmentId)
        {
           var currentAppointment= appointmentRepository.GetAppointment(AppointmentId);
            currentAppointment.UserId = null;
            currentAppointment.Appointed = false;
            appointmentRepository.Update(currentAppointment);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAvailable(string AppointmentId)
        {
            var currentAppointment = appointmentRepository.GetAppointment(AppointmentId);
            appointmentRepository.Delete(currentAppointment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditAvailable(string AppointmentId)
            {
            var currentAppointment = appointmentRepository.GetAppointment(AppointmentId);
            var model = new EditAppointmentViewModel
            {
                CounselorId= currentAppointment.CounselorId,
               Id=currentAppointment.Id,
                Date =currentAppointment.Date
            };
          
            return View(model);
        }


        [HttpPost]
            public IActionResult EditAvailable(EditAppointmentViewModel editAppointmentViewModel)
        {
            var currentAppointment = appointmentRepository.GetAppointment(editAppointmentViewModel.Id);
            currentAppointment.Date = editAppointmentViewModel.Date;
            appointmentRepository.Update(currentAppointment);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> RequestAppointment(string UserEmail)
        {
            var user = await userManager.FindByEmailAsync(UserEmail);
            var model = new AppointViewModel()
            {
                UserId = user.Id
            };
            return View( model);
        }

        [HttpGet]
        public IActionResult Payment(string Id)
        {
            ViewBag.AppointmentId = Id;
            return View();
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel model,string Id)
        {
            var payment = new Payment()
            {
                PaymentId=model.PaymentId,
                PaymentDate=DateTime.Now,
                Amount=10
            };
            context.payments.Add(payment);
            context.SaveChanges();
            
            return RedirectToAction("Appoint",new { Id });
        }


    }
}
