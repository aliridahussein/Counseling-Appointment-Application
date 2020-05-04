using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class Payment
    {
        [Required]
        [Display(Name = "Credit Card number")]
        [MaxLength(16, ErrorMessage ="Max Length is 16 degits")]
        public int PaymentId { get; set; }
        public int Amount { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }
    }
}
