using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class Users
    {
        public Users()
        {
            AccountDetails = new HashSet<AccountDetails>();
            CompanyUserMember = new HashSet<CompanyUserMember>();
            Invoice = new HashSet<Invoice>();
            Session = new HashSet<Session>();
            SessionAttendee = new HashSet<SessionAttendee>();
            SessionBooking = new HashSet<SessionBooking>();
            SessionDocument = new HashSet<SessionDocument>();
            SessionNotes = new HashSet<SessionNotes>();
            SessionRecording = new HashSet<SessionRecording>();
            UserBillingInformation = new HashSet<UserBillingInformation>();
            UserBlocked = new HashSet<UserBlocked>();
            UserCompany = new HashSet<UserCompany>();
            UserCreditCard = new HashSet<UserCreditCard>();
            UserFollower = new HashSet<UserFollower>();
            UserInterest = new HashSet<UserInterest>();
            UserLanguage = new HashSet<UserLanguage>();
            UserMembership = new HashSet<UserMembership>();
            UserPasswordRequest = new HashSet<UserPasswordRequest>();
            UserPractitoner = new HashSet<UserPractitoner>();
            UserProfile = new HashSet<UserProfile>();
            UserRole = new HashSet<UserRole>();
            UserSpeciality = new HashSet<UserSpeciality>();
        }

        public long UserId { get; set; }
        public Guid UserGuid { get; set; }
        public bool? IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? PasswordExpire { get; set; }
        public byte[] UserName { get; set; }
        public byte[] UserPassword { get; set; }
        public int? UnsuccessfulAttempt { get; set; }

        public virtual ICollection<AccountDetails> AccountDetails { get; set; }
        public virtual ICollection<CompanyUserMember> CompanyUserMember { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Session> Session { get; set; }
        public virtual ICollection<SessionAttendee> SessionAttendee { get; set; }
        public virtual ICollection<SessionBooking> SessionBooking { get; set; }
        public virtual ICollection<SessionDocument> SessionDocument { get; set; }
        public virtual ICollection<SessionNotes> SessionNotes { get; set; }
        public virtual ICollection<SessionRecording> SessionRecording { get; set; }
        public virtual ICollection<UserBillingInformation> UserBillingInformation { get; set; }
        public virtual ICollection<UserBlocked> UserBlocked { get; set; }
        public virtual ICollection<UserCompany> UserCompany { get; set; }
        public virtual ICollection<UserCreditCard> UserCreditCard { get; set; }
        public virtual ICollection<UserFollower> UserFollower { get; set; }
        public virtual ICollection<UserInterest> UserInterest { get; set; }
        public virtual ICollection<UserLanguage> UserLanguage { get; set; }
        public virtual ICollection<UserMembership> UserMembership { get; set; }
        public virtual ICollection<UserPasswordRequest> UserPasswordRequest { get; set; }
        public virtual ICollection<UserPractitoner> UserPractitoner { get; set; }
        public virtual ICollection<UserProfile> UserProfile { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<UserSpeciality> UserSpeciality { get; set; }
    }
}
