using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Models;
using E_CounsellingWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CounsellingWebApplication.Controllers
{
    [Authorize]
    public class CounselorsController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserCounselorRepository ICounselorRepository;

        public CounselorsController(RoleManager<IdentityRole> roleManager,
                                         UserManager<ApplicationUser> userManager,
                                      IUserCounselorRepository ICounselorRepository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.ICounselorRepository = ICounselorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string Email)
        {
            var currentUSer = await userManager.FindByEmailAsync(Email);
            ViewBag.UserId = currentUSer.Id;
            var list = ICounselorRepository.GetAllUserCounselors();
            var mylist = new List<ApplicationUser>();
            foreach (var item in list)
            {

                if (item.UserID == currentUSer.Id)
                {
                    var counselor = await userManager.FindByIdAsync(item.CounselorId);
                    mylist.Add(counselor);
                };
            }


            //var role = await roleManager.FindByIdAsync(roleId);
            var role = await roleManager.FindByNameAsync("Counselor");

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Name  cannot be found";
                return View("NotFound");
            }

            var model = new List<ApplicationUser>();

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Add(user);
                }

            }
            var CounselorListViewModel = new CouncelorsListViewModel
            {
                Counselors = model,
                MyCounselors = mylist

            };
            return View(CounselorListViewModel);
        }

        public async Task<IActionResult> AddCounselor(string userid, string CounselorId)
        {
            
            var user = await userManager.FindByEmailAsync(userid);
            var counselor = await userManager.FindByIdAsync(CounselorId);
            var usercounselor = new UserCounselors()
            {
                UserID = user.Id,
                CounselorId = CounselorId
            };
            ICounselorRepository.Add(usercounselor);
            return RedirectToAction("Index",new{ user.Email});
        }

        public async Task<IActionResult> RemoveCounselor(string CounselorId)
        {
            var currentUSer = await userManager.FindByEmailAsync(User.Identity.Name);
            //var user = await userManager.FindByEmailAsync(CounselorId);

            ICounselorRepository.Delete(CounselorId);
            return RedirectToAction("Index",new {currentUSer.Email });
        }

        public async Task<IActionResult> Endorse(float UserRating ,string userid,string counselorid)
        {
            var rate = new Ratings()
            {
                UserId = userid,
                CounselorId = counselorid,
                Rate= UserRating
            };
            ICounselorRepository.addRate(rate);
            float sum=0;
            var currentuser = await userManager.FindByIdAsync(userid);
            var user = await userManager.FindByIdAsync(counselorid);
            var list = ICounselorRepository.GetRatings(counselorid);
            var counselorlist = new List<Ratings>();
            foreach (var item in list)
            {
                if (item.CounselorId== counselorid)
                {
                    counselorlist.Add(item);
                    sum = sum+item.Rate;
                }
            }
                user.Rate = sum/ counselorlist.Count(); /*(user.Rate + UserRating)*/

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                
                return RedirectToAction("Index",new { currentuser.Email});
            }
            return RedirectToAction("Error");
        }

    }
}
