using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class SessionPayment
    {
        public long SessionPaymentId { get; set; }
        public Guid SessionPaymentGuid { get; set; }
        public long SessionId { get; set; }
        public int? SessionPaymentStatus { get; set; }
        public DateTime? SessionPaymentDate { get; set; }
        public long PractitionerUserId { get; set; }
        public long AttendeeMemberId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int? PaymentMethod { get; set; }
        public string PaymentMethodDetail1 { get; set; }
        public string PaymentMethodDetail2 { get; set; }
        public string PaymentNote { get; set; }
        public long? InvoiceId { get; set; }
        public int? IsDisputed { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Session Session { get; set; }
    }
}
