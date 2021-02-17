using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class DepartmentData
    {
        public int DepartmentId { get; set; }
        public string DepartmentGuid { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public int CreatedBy { get; set; }
     
    }
}
