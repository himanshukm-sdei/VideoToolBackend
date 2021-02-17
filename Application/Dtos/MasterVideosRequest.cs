using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class MasterVideosRequest
    {
        public string VideosName { get; set; }
        public Guid sessionGuid { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
    }
}
