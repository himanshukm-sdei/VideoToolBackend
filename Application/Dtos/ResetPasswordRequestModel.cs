using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
   public class ResetPasswordRequestModel
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SecretKey { get; set; }

    }
}
