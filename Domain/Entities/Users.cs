using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {

        public long UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool? IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }
        public string Message { get; set; }
        public bool IsDeactivated { get; set; }
        public bool? IsDeleted { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public string Firstname { get; set; }


    }
}
