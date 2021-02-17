using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class ChangePasswordRequestModel
    {
        public long UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string SecretKey { get; set; }

    }
}
