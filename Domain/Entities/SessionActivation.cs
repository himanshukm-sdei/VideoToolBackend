using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class SessionActivation
    {
        public long SessionId { get; set; }
        public bool? IsSessionStarted { get; set; }
        public int VideosId { get; set; }
        public string VideosSrc { get; set; }
    }
}
