using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class AccountDetails
    {
        public long AccountId { get; set; }
        public Guid AccountGuid { get; set; }
        public long UserId { get; set; }
        public byte[] AccountType { get; set; }
        public byte[] BankName { get; set; }
        public byte[] HolderName { get; set; }
        public byte[] AccountNumber { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Users User { get; set; }
    }
}
