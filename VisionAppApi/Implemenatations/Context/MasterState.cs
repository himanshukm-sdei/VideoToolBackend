using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterState
    {
        public MasterState()
        {
            Company = new HashSet<Company>();
            CompanyBillingInformation = new HashSet<CompanyBillingInformation>();
            UserBillingInformation = new HashSet<UserBillingInformation>();
        }

        public long StateId { get; set; }
        public string StateName { get; set; }
        public long? CountryId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<CompanyBillingInformation> CompanyBillingInformation { get; set; }
        public virtual ICollection<UserBillingInformation> UserBillingInformation { get; set; }
    }
}
