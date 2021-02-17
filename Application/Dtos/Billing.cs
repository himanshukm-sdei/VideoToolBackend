using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
  public  class Billing
    {
        public long UserBillingInformationId { get; set; }
        public long? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public long? State { get; set; }
        public long? Country { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
    }
}
