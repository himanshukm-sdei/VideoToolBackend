using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserCreditCard
    {
        public long UserCreditCardId { get; set; }
        public Guid UserCreditCardGuid { get; set; }
        public long UserId { get; set; }
        public bool? IsPrimary { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CardId { get; set; }
        public string TokenId { get; set; }
        public byte[] NameOnCard { get; set; }

        public virtual Users User { get; set; }
    }
}
