using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.Dtos
{
    public class SessionRequest
    {
        [Required]
        public int UserId { get; set; }
        [MaxLength(50)]
        public string SessionTitle { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int SessionType { get; set; }
        [MaxLength(500)]
        public string SessionShotDescription { get; set; }
        public string SessionDescription { get; set; }
        [Required]
        public DateTime? SessionDate { get; set; }
        [Required]
        public DateTime? SessionTime { get; set; }
        public int? SessionLength { get; set; }
        public int? NumberOfSeats { get; set; }
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Target Price; Max 18 digits")]
        public decimal? SeatPrice { get; set; }
        [Required]
        public bool IsWaitingList { get; set; }
        [Required]
        public bool IsWaitingVideoRecording { get; set; }
        public int[] Tags { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishTime { get; set; }
        public int? FirstResponder { get; set; }

        public int? CreatedBy { get; set; }

        public int sessionId { get; set; }

        public int TimezoneId { get; set; }

        public int IsAccepted { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public long? AcceptedBy { get; set; }

    }

    public class sessionstatusData
    {
        public int sessionId { get; set; }
        public int IsAccepted { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public long? AcceptedBy { get; set; }

    }
}
