using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterTopic
    {
        public MasterTopic()
        {
            Session = new HashSet<Session>();
        }

        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string TopicImage { get; set; }

        public virtual ICollection<Session> Session { get; set; }
    }
}
