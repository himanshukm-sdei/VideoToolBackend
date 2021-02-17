using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class Invoice
    {
        public decimal? InvoiceAmount { get; set; }
        public string InvoiceFirstName { get; set; }
        public string InvoiceLastName { get; set; }
        public string Address1 { get; set; }
        public int? TranscationType { get; set; }
        public long? TranscationId { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public long InvoiceId { get; set; }
        public Guid InvoiceGuid { get; set; }
        public long UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
