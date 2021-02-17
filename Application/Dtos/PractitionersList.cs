using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class PractitionersList
    {
        //PRACTIONERS list Model
        public long UserId { get; set; }
        public long ProfileId { get; set; }
        public Guid ProfileGuid { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PractitionerSince { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsBlocked { get; set; }
        public int Total { get; set; }
        public int SessionAssigned { get; set; }
        public int TimezoneId { get; set; }
        public string PractitionerName{ get; set; }



    }
}
