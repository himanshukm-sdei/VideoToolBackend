using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterVideos
    {
        public MasterVideos()
        {
            SessionVideos = new HashSet<SessionVideos>();
        }

        public int VideosId { get; set; }
        public Guid VideosGuid { get; set; }
        public string VideosName { get; set; }
        public string VideosFileName { get; set; }
        public string VideosLength { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsDefault { get; set; }

        public virtual ICollection<SessionVideos> SessionVideos { get; set; }
    }
}
