using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserLanguage
    {
        public long UserLanguageId { get; set; }
        public long UserId { get; set; }
        public int LanguageId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual MasterLanguage Language { get; set; }
        public virtual Users User { get; set; }
    }
}
