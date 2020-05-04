using System;
using System.IO;
using System.Reflection.Metadata;
using E_CounsellingWebApplication.Models;
using E_CounsellingWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_CounsellingWebApplication.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IBookRepository repository;

        public BooksController(IHostingEnvironment hostingEnvironment,
                                IBookRepository repository)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var list = repository.GetAllBooks();
            
            return View(list);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.File != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Books");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var user = new Book()
                {
                    BookName = model.Name,
                    Link = uniqueFileName,
                    Description = model.Description
                };
                repository.Add(user);
                return RedirectToAction("Index");
            }
               
            
            return View(model);

        }

        

        public ActionResult Download(string filename)
        {
            string path = "wwwroot/Books/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + filename);
            string fileName = filename;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public IActionResult Delete(string fileid)
        {
           
            repository.Delete(fileid);
            return RedirectToAction("Index");
        }


    }
}
