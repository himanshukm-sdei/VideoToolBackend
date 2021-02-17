using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterLanguage
    {
        public MasterLanguage()
        {
            UserLanguage = new HashSet<UserLanguage>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<UserLanguage> UserLanguage { get; set; }
    }
}
