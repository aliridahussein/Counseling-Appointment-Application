﻿using E_CounsellingWebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Hubs
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }

        public string UserID { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public Message()
        {
            When = DateTime.Now;
        }
    }


}
