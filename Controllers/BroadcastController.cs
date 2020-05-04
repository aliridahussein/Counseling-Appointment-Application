using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Models;
using E_CounsellingWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_CounsellingWebApplication.Controllers
{
    [Authorize]
    public class BroadcastController : Controller
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public BroadcastController(IBroadcastRepository broadcastRepository,
                                    UserManager<ApplicationUser> userManager)
        {
            _broadcastRepository = broadcastRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (sortOrder==null)
            {
                sortOrder = "Date";
            }
            ViewBag.CurrentFilter = searchString;
            var model = _broadcastRepository.GetAllBroadcasts();
            var List = new List<BroadcastListViewModel>();
            foreach (var broadCast in model)
            {
                var user = await userManager.FindByIdAsync(broadCast.UserId);
                var broadcast = new BroadcastListViewModel
                {
                    BroadcastId = broadCast.BroadcastId,
                    Date = broadCast.Date,
                    Text = broadCast.Text,
                    UserId=broadCast.UserId,
                    PhotoPath=user.PhotoPath,
                    FirstName= user.FirstName,
                    LastName=user.LastName
                    
                };
                List.Add(broadcast);
                
            };

            IEnumerable<BroadcastListViewModel> sortedlist = Enumerable.Empty<BroadcastListViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                sortedlist = List.Where(B => B.FirstName.ToLower().Contains(searchString.ToLower())
                     || B.LastName.ToLower().Contains(searchString.ToLower()));
            }
            else
	        {
                sortedlist = List;
            };
            switch (sortOrder)
            {
                case "name_desc":
                    sortedlist = sortedlist.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    sortedlist = sortedlist.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    sortedlist = sortedlist.OrderByDescending(s => s.Date);
                    break;
                default:  // Name ascending 
                    sortedlist = sortedlist.OrderBy(s => s.LastName);
                    break;
            }
            return View(sortedlist);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Email = {Email} cannot be found";
                return View("NotFound");
            }
            ViewBag.Id = user.Id;
            return View();
        }

        [HttpPost]
        public  IActionResult Create(Broadcast model)
        {
            if (ModelState.IsValid)
            {
                Broadcast broadcast = _broadcastRepository.Add(model);
                //user.broadcasts.Add(model);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
