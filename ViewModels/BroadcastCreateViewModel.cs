using E_CounsellingWebApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.ViewModels
{
    public class BroadcastCreateViewModel
    {
       [Required]
       
        public string BroadcastId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
    }
}
