using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class DepartmentsList
    {
        //Company Model
        public int DepartmentId { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }

    }
}
