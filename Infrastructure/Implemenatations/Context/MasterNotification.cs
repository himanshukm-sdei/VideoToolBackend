using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterNotification
    {
        public MasterNotification()
        {
            UserNotification = new HashSet<UserNotification>();
        }

        public long NotificationId { get; set; }
        public Guid NotificationGuid { get; set; }
        public long NotificationTypeId { get; set; }
        public string NotificationTemplate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long NotificationIntervals { get; set; }
        public string NotificationIntervalType { get; set; } 
        public string NotificationSubject { get; set; }


        public virtual MasterNotificationType NotificationType { get; set; }
        public virtual ICollection<UserNotification> UserNotification { get; set; }
    }
}
