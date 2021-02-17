using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterNotificationType
    {
        public MasterNotificationType()
        {
            MasterNotification = new HashSet<MasterNotification>();
        }

        public long NotificationTypeId { get; set; }
        public Guid NotificationTypeGuid { get; set; }
        public string NotificationTypeName { get; set; }

        public virtual ICollection<MasterNotification> MasterNotification { get; set; }
    }
}
