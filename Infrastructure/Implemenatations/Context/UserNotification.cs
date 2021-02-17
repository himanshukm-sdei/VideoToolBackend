using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserNotification
    {
        public long UserNotificationId { get; set; }
        public Guid UserNotificationGuid { get; set; }
        public long NotificationId { get; set; }
        public long UserNotificationStatus { get; set; }
        public DateTime? UserNotificationSentDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long UserId { get; set; }
        public string UserNotificationText { get; set; }
        public virtual MasterNotification Notification { get; set; }
    }
}
