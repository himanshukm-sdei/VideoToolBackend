using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;

namespace Application.Dtos
{
    public class SignupRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Specialities { get; set; }
        public long? Timezone { get; set; }
        [MaxLength(500)]
        public string BriefBio { get; set; }
        [MaxLength(200)]
        public string InstagramProfile { get; set; }
        [MaxLength(200)]
        public string LinkedInProfile { get; set; }
        [MaxLength(200)]
        public string FaceBookProfile { get; set; }
        [Required]
        public string SecretKey { get; set; }
        public long? CreatedBy { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string FromAdmin { get; set; }
    }
}
