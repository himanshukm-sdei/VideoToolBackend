using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class ProfileInfo
    {
        [Required]
        public long ProfileId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public DateTime? PractitionerSince { get; set; }
        [MaxLength(12)]
        public string ZipCode { get; set; }

        public List<Specialities> Specialities { get; set; }
        [MaxLength(500)]
        public string Qualification { get; set; }
        [MaxLength(500)]
        public string Education { get; set; }
        public int? IsFeature { get; set; }
        public int? Ranked { get; set; }
        [MaxLength(500)]
        public string BriefBio { get; set; }
        [MaxLength(200)]
        public string InstagramProfile { get; set; }
        [MaxLength(200)]
        public string LinkedInProfile { get; set; }
        [MaxLength(200)]
        public string FaceBookProfile { get; set; }
        [MaxLength(15)]
        public string ContactNo1 { get; set; }
        [MaxLength(15)]
        public string ContactNo2 { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(80)]
        public string Language { get; set; }
        public string ProfilePhoto { get; set; }
        public string CountrName { get; set; }
        public string StateName { get; set; }
        public string DepartmentName { get; set; }
        public bool? PublicProfile { get; set; }
        public int? FirstResponder { get; set; }
        public long? DepartmentId { get; set; }
        [MaxLength(200)]
        public string Position { get; set; }
        [Required]
        public long UserId { get; set; }
        public string EmployeeNumber { get; set; }
        public bool? AllowDepartment { get; set; }
        public long TimezoneId { get; set; }
        public string UserTypeText { get; set; }
        public string TimezoneName { get; set; }
    }

    public class Specialities
    {
        public int? SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public long UserId { get; set; }
    }
}
