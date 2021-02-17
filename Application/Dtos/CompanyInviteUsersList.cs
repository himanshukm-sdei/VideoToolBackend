using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class CompanyInviteUsersList
    {
        //Company Members list Model
        //Company Model
        public Guid UserInviteGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Guid ActivationToken { get; set; }
        public DateTime InviteSentDate { get; set; }
        public int InviteSentStatus { get; set; }
        public int Total { get; set; }

    }
}
