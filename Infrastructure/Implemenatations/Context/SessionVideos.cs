using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class SessionVideos
    {
        public long SessionVideosId { get; set; }
        public Guid SessionVideosGuid { get; set; }
        public long SessionId { get; set; }
        public int VideosId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual MasterVideos Videos { get; set; }
    }
}
