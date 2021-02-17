using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class CompanyMembersList
    {
        //Company Members list Model
        public long CompanyId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MemberSince { get; set; }
        public bool? IsActive { get; set; }

    }
}
