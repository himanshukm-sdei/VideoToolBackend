using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterUserLogActivity
    {
        public MasterUserLogActivity()
        {
            UserLogActivity = new HashSet<UserLogActivity>();
        }

        public int LogActivityId { get; set; }
        public string LogActivityName { get; set; }

        public virtual ICollection<UserLogActivity> UserLogActivity { get; set; }
    }
}
