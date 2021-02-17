using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class SessionTypeData
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int CreatedBy { get; set; }
     
    }
}
