using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class Company
    {
        public Company()
        {
            CompanyBillingInformation = new HashSet<CompanyBillingInformation>();
            CompanyRatePlan = new HashSet<CompanyRatePlan>();
            CompanyUserMember = new HashSet<CompanyUserMember>();
            UserCompany = new HashSet<UserCompany>();
            UserInvites = new HashSet<UserInvites>();
        }

        public long CompanyId { get; set; }
        public Guid CompanyGuid { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyCity { get; set; }
        public long? CompanyState { get; set; }
        public long? CompanyCountry { get; set; }
        public string CompanyZipCode { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyLogo { get; set; }
        public bool? IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public string CompanyUserCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public byte[] CompanyName { get; set; }
        public byte[] CompanyAddress { get; set; }
        public byte[] CompanyPhone { get; set; }
        public string CompanyUserNameCode { get; set; }
        public int? RepresentativeId { get; set; }

        public virtual MasterCountry CompanyCountryNavigation { get; set; }
        public virtual MasterState CompanyStateNavigation { get; set; }
        public virtual Representative Representative { get; set; }
        public virtual ICollection<CompanyBillingInformation> CompanyBillingInformation { get; set; }
        public virtual ICollection<CompanyRatePlan> CompanyRatePlan { get; set; }
        public virtual ICollection<CompanyUserMember> CompanyUserMember { get; set; }
        public virtual ICollection<UserCompany> UserCompany { get; set; }
        public virtual ICollection<UserInvites> UserInvites { get; set; }
    }
}
