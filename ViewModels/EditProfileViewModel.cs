using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
        public string Category { get; set; }
    }
}
