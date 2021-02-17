using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterTimezone
    {
        public MasterTimezone()
        {
            UserProfile = new HashSet<UserProfile>();
        }

        public long TimezoneId { get; set; }
        public string TimezoneValue { get; set; }
        public string TimezoneText { get; set; }
        public bool IsActive { get; set; }
        public string TimeZoneName { get; set; }

        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
