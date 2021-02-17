using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class UserBillingInformation
    {
        public long UserBillingInformationId { get; set; }
        public Guid UserBillingInformationGuid { get; set; }
        public string City { get; set; }
        public long? State { get; set; }
        public long? Country { get; set; }
        public string ZipCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public long UserId { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public byte[] Address { get; set; }

        public virtual MasterCountry CountryNavigation { get; set; }
        public virtual MasterState StateNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
