using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterEmailTemplate
    {
        public int EmailTemplateId { get; set; }
        public string EmailTemplateName { get; set; }
        public string EmailTemplateNote { get; set; }
        public byte[] EmailTemplateFileName { get; set; }
        public byte[] EmailTemplateSubject { get; set; }
    }
}
