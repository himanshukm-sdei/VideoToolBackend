using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class InviteEmailData
    {
        //Company Model
        public Guid UserInviteGuId { get; set; }
        public string EmailSender { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int InviteStatus { get; set; }
        public string EmailReceiver { get; set; }
       
        public int UserId { get; set; }
        public int EmailTemplateId { get; set; }


    }
}
