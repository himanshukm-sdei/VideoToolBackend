using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ListVm
    {
        public int Value { get; set; }
        public string ViewValue { get; set; }
        public int ForeignValue { get; set; }
    }

    public class DepartmentListVm
    {
        public int Value { get; set; }
        public string ViewValue { get; set; }
        public string Code { get; set; }
    }
}
