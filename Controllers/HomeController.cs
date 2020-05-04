using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_CounsellingWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_CounsellingWebApplication.Hubs;

namespace E_CounsellingWebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext context;

        public HomeController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                               AppDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }
        public async Task<IActionResult> LiveChat()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentName = currentUser.UserName;
            }
           
            return View();
        }

        
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            var messages = await context.Messages.ToListAsync();
            return View(messages);
        }


        [AllowAnonymous]
        public IActionResult Welcome()
        {
            if(signInManager.IsSignedIn(User)){
                return RedirectToAction("Index");
            }
            else
	        {
                return View();
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ContactUs()
        {
           return View();
        }




    }
}
