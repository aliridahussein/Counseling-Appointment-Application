using E_CounsellingWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.ViewModels
{
    public class CouncelorsListViewModel
    {
        public List<ApplicationUser> Counselors { get; set; }
        public List<ApplicationUser> MyCounselors { get; set; }
    }
}
