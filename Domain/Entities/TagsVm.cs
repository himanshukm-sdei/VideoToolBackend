using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TagsVm
    {
        public long SessionTagId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
        public long SessionId { get; set; }
    }
}
