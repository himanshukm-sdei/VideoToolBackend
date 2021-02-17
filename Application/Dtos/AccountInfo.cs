using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class AccountInfo
    {
        public long AccountId { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string HolderName { get; set; }
        public string AccountNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
