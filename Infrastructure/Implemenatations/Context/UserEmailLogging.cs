using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserEmailLogging
    {
        public Guid UserEmailGuid { get; set; }
        public string EmailReceiver { get; set; }
        public string EmailSender { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int EmailSentStatus { get; set; }
        public DateTime EmailSentDate { get; set; }
        public long? UserId { get; set; }
        public int EmailTemplateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual MasterEmailTemplate EmailTemplate { get; set; }
    }
}
