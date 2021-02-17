using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
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
        public string NotificationCategory { get; set; }
        public bool? NotificationTypeBeforeAfter { get; set; }
        public string NotificationApi { get; set; }

        public virtual ICollection<MasterNotification> MasterNotification { get; set; }
    }
}
