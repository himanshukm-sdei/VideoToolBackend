using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class UserEmailLogging
    {
        public Guid UserEmailGuid { get; set; }
        public string EmailReceiver { get; set; }
        public string EmailSender { get; set; }
        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

        public int EmailSentStatus { get; set; }
        public DateTime EmailSentDate { get; set; }
        public int UserId { get; set; }
        public int EmailTemplateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string EmailTemplateName { get; set; }
        

    }
}
