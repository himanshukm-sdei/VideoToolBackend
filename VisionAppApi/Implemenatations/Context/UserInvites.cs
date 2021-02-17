using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class UserInvites
    {
        public Guid UserInviteGuid { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public long? CompanyId { get; set; }
        public byte[] EmailAddress { get; set; }
        public Guid ActivationToken { get; set; }
        public DateTime? InviteSentDate { get; set; }
        public DateTime? InviteAcceptedDate { get; set; }
        public int? InviteStatus { get; set; }
        public int? InviteSentStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public long? UserId { get; set; }
        public string EmailSender { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }

        public virtual Company Company { get; set; }
    }
}
