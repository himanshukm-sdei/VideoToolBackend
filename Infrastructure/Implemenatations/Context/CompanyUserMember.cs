﻿using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class CompanyUserMember
    {
        public long LogId { get; set; }
        public Guid LogGuid { get; set; }
        public long CompanyId { get; set; }
        public long UserId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Company Company { get; set; }
        public virtual Users User { get; set; }
    }
}
