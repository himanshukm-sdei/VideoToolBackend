using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class TopicRequest
    {
        public string TopicName { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
    }
}
