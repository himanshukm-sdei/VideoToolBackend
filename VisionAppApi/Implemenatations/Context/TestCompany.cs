﻿using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class TestCompany
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentCompanyId { get; set; }
    }
}
