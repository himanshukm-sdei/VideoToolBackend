using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class PractitionersRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Keywords { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
    }
}
