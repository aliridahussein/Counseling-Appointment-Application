using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Models;
using Microsoft.AspNetCore.Http;

namespace E_CounsellingWebApplication.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of birth")]
        public DateTime DOB { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse",controller:"Account")] //to check if used in clientside not serverside
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation doesn't match.")]
        public string ConfirmPassword { get; set; }
        public IFormFile Photo { get; set; }
    }

   
}
