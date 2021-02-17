using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class UserProfile
    {
        public long ProfileId { get; set; }
        public Guid ProfileGuid { get; set; }
        public long UserId { get; set; }
        public string ProfilePhoto { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string ZipCode { get; set; }
        public string Qualification { get; set; }
        public string Education { get; set; }
        public int? IsFeature { get; set; }
        public int? Ranked { get; set; }
        public string BriefBio { get; set; }
        public string InstagramProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public string FaceBookProfile { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public long? IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsDeactivated { get; set; }
        public DateTime? PractitionerSince { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public bool? PublicProfile { get; set; }
        public int? FirstResponder { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public byte[] Email { get; set; }
        public byte[] ContactNo1 { get; set; }
        public byte[] ContactNo2 { get; set; }
        public long? DepartmentId { get; set; }
        public byte[] Position { get; set; }
        public string EmployeeNumber { get; set; }
        public bool? AllowDepartment { get; set; }
        public long? TimezoneId { get; set; }
        public string UserTypeText { get; set; }

        public virtual MasterDepartment Department { get; set; }
        public virtual MasterTimezone Timezone { get; set; }
        public virtual Users User { get; set; }
    }
}
