using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class SessionDocument
    {
        public long SessionDocumentId { get; set; }
        public Guid SessionDocumentGuid { get; set; }
        public long SessionId { get; set; }
        public long UserId { get; set; }
        public string DocumentName { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Session Session { get; set; }
        public virtual Users User { get; set; }
    }
}
