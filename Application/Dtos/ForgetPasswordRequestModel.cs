using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class ForgetPasswordRequestModel
    {
        public string SecretKey { get; set; }
        public string EmailId { get; set; }
        public long UserId { get; set; }
        public long TemplateID { get; set; }

    }
}
