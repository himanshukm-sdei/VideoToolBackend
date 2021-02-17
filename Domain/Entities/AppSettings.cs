using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AppSettings
    {
        public string Key { get; set; }
        public string SecretKey { get; set; }
        public string StripePublishKey { get; set; }
        public string StripeSecretKey { get; set; }
    }
}
