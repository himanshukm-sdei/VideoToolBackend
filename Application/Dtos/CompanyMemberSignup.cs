using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class CompanyMemberSignup
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserPassword { get; set; }
        public long? TimezoneId { get; set; }
        public string SecretKey { get; set; }
        [Required]
        public Guid InvitedGuid { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUserNameCode { get; set; }
        public int CompanyId { get; set; }
        public int CompanyAdminId { get; set; }
        public string Description { get; set; }

    }
}
