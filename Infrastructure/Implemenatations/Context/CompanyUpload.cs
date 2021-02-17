using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class CompanyUpload
    {
        public long UploadId { get; set; }
        public Guid UploadGuid { get; set; }
        public long? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int StatusCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
