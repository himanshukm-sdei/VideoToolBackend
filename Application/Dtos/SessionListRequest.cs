using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class SessionListRequest
    {
        public int? PractitionerId { get; set; }
        public string Topic { get; set; }
        public int? TopicId { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string PractitionerName { get; set; }
        public bool Open { get; set; }
        public bool Private { get; set; }
        public bool? Today { get; set; }
        public bool? Next { get; set; }
        public bool onlyConfirmed { get; set; }
        public bool? Previous { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MemberId { get; set; }
    }
    public class FilteredSessionListRequest
    {
        public int PractitionerId { get; set; }
        public string Category { get; set; }
        public string SessionName { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
        public int? MemberId { get; set; }
        public int? SessionValue { get; set; }
        public bool? FirstResponder { get; set; }
        public bool? IsApproved { get; set; }
        public int? SessionStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? Today { get; set; }
        public int? Previous { get; set; }
        public int? Next { get; set; }
        public string Tags { get; set; }
        public int? SessionDeliveryStatus { get; set; }
        public int? SessionApprovalStatus { get; set; }

    }
}
