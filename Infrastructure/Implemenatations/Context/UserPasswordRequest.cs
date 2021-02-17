using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserPasswordRequest
    {
        public long PasswordRequestId { get; set; }
        public Guid? PasswordRequestGuid { get; set; }
        public long UserId { get; set; }
        public DateTime? PasswordRequestDate { get; set; }
        public int? PasswordRequestStatus { get; set; }
        public string PasswordRequestLink { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsResetPasswordLinkClicked { get; set; }

        public virtual Users User { get; set; }
    }
}
