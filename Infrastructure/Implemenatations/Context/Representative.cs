using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class Representative
    {
        public Representative()
        {
            Company = new HashSet<Company>();
        }

        public int RepresentativeId { get; set; }
        public string RepresentativeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Company> Company { get; set; }
    }
}
