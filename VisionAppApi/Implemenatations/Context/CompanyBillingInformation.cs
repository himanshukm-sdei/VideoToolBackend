using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class CompanyBillingInformation
    {
        public long CompanyBillingInformationId { get; set; }
        public Guid CompanyBillingInformationGuid { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public byte[] CompanyBillingName { get; set; }
        public byte[] Address { get; set; }
        public string City { get; set; }
        public long? State { get; set; }
        public long? Country { get; set; }
        public string ZipCode { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Company Company { get; set; }
        public virtual MasterCountry CountryNavigation { get; set; }
        public virtual MasterState StateNavigation { get; set; }
    }
}
