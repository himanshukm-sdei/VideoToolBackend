using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class ResetPasswordResponseModel
    {
        public long UserId { get; set; }
        public string Guid { get; set; }

    }
}
