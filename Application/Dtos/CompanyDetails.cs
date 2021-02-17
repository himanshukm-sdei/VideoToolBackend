using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class CompanyDetails
    {
        //Company Model
        public long CompanyId { get; set; }
        public Guid CompanyGuid { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
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
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public int RepresentativeId { get; set; }

        // Company Billing Model
        public long CompanyBillingInformationId { get; set; }
        public Guid CompanyBillingInformationGuId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyBillingName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string CompanyID { get; set; }

        // Company Billing Rate Model
        public int PlanID { get; set; }

        //Company Admin Contact Model
        public long ProfileId { get; set; }
        public Guid ProfileGuid { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public int UserId { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string WorkPhoneNo { get; set; }
        public string MobilePhoneNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
 
    }
}
