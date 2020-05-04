using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace E_CounsellingWebApplication.Controllers
{
    [Authorize]
    public class CategoriesController : Controller   
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(IHostingEnvironment hostingEnvironment,
                                     ICategoryRepository categoryRepository)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult Index()
        {
            var model = categoryRepository.GetAllCategories();
            return View("~/Views/Categories/Index.cshtml", model);
        }

        public ViewResult Lifestyle()
        {
            return View();
        }

        public ViewResult Business()
        {
            return View();
        }

        public ViewResult Depression()
        {
            return View();
        }


    }
}