using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class TopicList
    {
        public long TopicId { get; set; }
        public long SessionCount { get; set; }
        public string TopicName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int Total { get; set; }
    }
}
