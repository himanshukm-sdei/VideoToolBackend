using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterSpeciality
    {
        public MasterSpeciality()
        {
            UserSpeciality = new HashSet<UserSpeciality>();
        }

        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<UserSpeciality> UserSpeciality { get; set; }
    }
}
