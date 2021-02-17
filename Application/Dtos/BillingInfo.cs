using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class BillingInfo
    {
       
        [Required]
        public int UserId { get; set; }
        public long UserCreditCardId { get; set; }
     
        [Required]
        public string NameOnCard { get; set; }
        public bool? IsPrimary { get; set; }
        [Required]
        public string TokenId { get; set; }
        [Required]
        public string CardId { get; set; }
      
    
    }
}
