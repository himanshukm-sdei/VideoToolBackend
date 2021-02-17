using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterCountry
    {
        public MasterCountry()
        {
            Company = new HashSet<Company>();
            CompanyBillingInformation = new HashSet<CompanyBillingInformation>();
            MasterState = new HashSet<MasterState>();
            UserBillingInformation = new HashSet<UserBillingInformation>();
        }

        public long CountryId { get; set; }
        public string CountrName { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<CompanyBillingInformation> CompanyBillingInformation { get; set; }
        public virtual ICollection<MasterState> MasterState { get; set; }
        public virtual ICollection<UserBillingInformation> UserBillingInformation { get; set; }
    }
}
