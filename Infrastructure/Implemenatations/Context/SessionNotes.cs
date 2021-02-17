using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class SessionNotes
    {
        public long SessionNotesId { get; set; }
        public Guid SessionNotesGuid { get; set; }
        public long SessionId { get; set; }
        public long UserId { get; set; }
        public string Notes { get; set; }
        public DateTime? NotesDates { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Session Session { get; set; }
        public virtual Users User { get; set; }
    }
}
