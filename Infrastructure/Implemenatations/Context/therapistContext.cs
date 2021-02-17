using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Implemenatations.Context
{
    public partial class therapistContext : DbContext
    {
        public therapistContext()
        {
        }

        public therapistContext(DbContextOptions<therapistContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDetails> AccountDetails { get; set; }
        public virtual DbSet<BlockUserHistory> BlockUserHistory { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyBillingInformation> CompanyBillingInformation { get; set; }
        public virtual DbSet<CompanyRatePlan> CompanyRatePlan { get; set; }
        public virtual DbSet<CompanyUpload> CompanyUpload { get; set; }
        public virtual DbSet<CompanyUserMember> CompanyUserMember { get; set; }
        public virtual DbSet<ElmahError> ElmahError { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<MasterCountry> MasterCountry { get; set; }
        public virtual DbSet<MasterDepartment> MasterDepartment { get; set; }
        public virtual DbSet<MasterEmailTemplate> MasterEmailTemplate { get; set; }
        public virtual DbSet<MasterInterest> MasterInterest { get; set; }
        public virtual DbSet<MasterLanguage> MasterLanguage { get; set; }
        public virtual DbSet<MasterNotification> MasterNotification { get; set; }
        public virtual DbSet<MasterNotificationType> MasterNotificationType { get; set; }
        public virtual DbSet<MasterPlan> MasterPlan { get; set; }
        public virtual DbSet<MasterRole> MasterRole { get; set; }
        public virtual DbSet<MasterSessionType> MasterSessionType { get; set; }
        public virtual DbSet<MasterSpeciality> MasterSpeciality { get; set; }
        public virtual DbSet<MasterState> MasterState { get; set; }
        public virtual DbSet<MasterTag> MasterTag { get; set; }
        public virtual DbSet<MasterTimezone> MasterTimezone { get; set; }
        public virtual DbSet<MasterTopic> MasterTopic { get; set; }
        public virtual DbSet<MasterUserLogActivity> MasterUserLogActivity { get; set; }
        public virtual DbSet<MasterVideos> MasterVideos { get; set; }
        public virtual DbSet<Representative> Representative { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<SessionAttendee> SessionAttendee { get; set; }
        public virtual DbSet<SessionBooking> SessionBooking { get; set; }
        public virtual DbSet<SessionDocument> SessionDocument { get; set; }
        public virtual DbSet<SessionNotes> SessionNotes { get; set; }
        public virtual DbSet<SessionPayment> SessionPayment { get; set; }
        public virtual DbSet<SessionRecording> SessionRecording { get; set; }
        public virtual DbSet<SessionTag> SessionTag { get; set; }
        public virtual DbSet<SessionVideos> SessionVideos { get; set; }
        public virtual DbSet<TestCompany> TestCompany { get; set; }
        public virtual DbSet<TestEmployees> TestEmployees { get; set; }
        public virtual DbSet<UserBillingInformation> UserBillingInformation { get; set; }
        public virtual DbSet<UserBlocked> UserBlocked { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }
        public virtual DbSet<UserCreditCard> UserCreditCard { get; set; }
        public virtual DbSet<UserEmailLogging> UserEmailLogging { get; set; }
        public virtual DbSet<UserFollower> UserFollower { get; set; }
        public virtual DbSet<UserInterest> UserInterest { get; set; }
        public virtual DbSet<UserInvites> UserInvites { get; set; }
        public virtual DbSet<UserLanguage> UserLanguage { get; set; }
        public virtual DbSet<UserLogActivity> UserLogActivity { get; set; }
        public virtual DbSet<UserMembership> UserMembership { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<UserPasswordRequest> UserPasswordRequest { get; set; }
        public virtual DbSet<UserPractitoner> UserPractitoner { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserSpeciality> UserSpeciality { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=75.126.168.31,7009;Database=therapist;uid=therapist;password=therapist;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDetails>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__AccountD__349DA5A6FED4A303");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AccountDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountDe__UserI__5CF6C6BC");
            });

            modelBuilder.Entity<BlockUserHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK__BlockUse__4D7B4ABDF4E83334");

                entity.Property(e => e.BlockedUserId).HasColumnName("BlockedUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UnblockedUserId).HasColumnName("UnblockedUserID");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyCity)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyLogo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyUserCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyUserNameCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyWebsite)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyZipCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CompanyCountryNavigation)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CompanyCountry)
                    .HasConstraintName("FK__Company__Company__5EDF0F2E");

                entity.HasOne(d => d.CompanyStateNavigation)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CompanyState)
                    .HasConstraintName("FK__Company__Company__5DEAEAF5");

                entity.HasOne(d => d.Representative)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.RepresentativeId)
                    .HasConstraintName("FK__Company__Represe__5FD33367");
            });

            modelBuilder.Entity<CompanyBillingInformation>(entity =>
            {
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyBillingInformation)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__CompanyBi__Compa__60C757A0");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.CompanyBillingInformation)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK__CompanyBi__Count__61BB7BD9");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.CompanyBillingInformation)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("FK__CompanyBi__State__62AFA012");
            });

            modelBuilder.Entity<CompanyRatePlan>(entity =>
            {
                entity.HasKey(e => e.CompanyRateTableId)
                    .HasName("PK__CompanyR__56032F6300EDF425");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PlanEndDate).HasColumnType("datetime");

                entity.Property(e => e.PlanStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyRatePlan)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyRa__Compa__63A3C44B");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.CompanyRatePlan)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__CompanyRa__PlanI__6497E884");
            });

            modelBuilder.Entity<CompanyUpload>(entity =>
            {
                entity.HasKey(e => e.UploadId)
                    .HasName("PK__CompanyU__6D16C84D7D4383E2");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompanyUserMember>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__CompanyU__5E54864841CDD651");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyUserMember)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyUs__Compa__658C0CBD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CompanyUserMember)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyUs__UserI__668030F6");
            });

            modelBuilder.Entity<ElmahError>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ELMAH_Error");

                entity.Property(e => e.AllXml)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Application)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Sequence).ValueGeneratedOnAdd();

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.TimeUtc).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Address1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TranscationId).HasColumnName("TranscationID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__UserId__6774552F");
            });

            modelBuilder.Entity<MasterCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__MasterCo__10D1609F041A5EF5");

                entity.Property(e => e.CountrName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("PK__MasterDe__B2079BEDA114AB20");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterEmailTemplate>(entity =>
            {
                entity.HasKey(e => e.EmailTemplateId)
                    .HasName("PK__MasterEm__BC0A387544E0213E");

                entity.Property(e => e.EmailTemplateFileName).HasMaxLength(100);

                entity.Property(e => e.EmailTemplateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTemplateNote)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTemplateSubject).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterInterest>(entity =>
            {
                entity.HasKey(e => e.InterestId)
                    .HasName("PK__MasterIn__20832C67DAFE7D81");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InterestName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("PK__MasterLa__B93855AB901BC32F");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("PK__MasterNo__20CF2E1251835444");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NotificationIntervalType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationSubject)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Session Reminder')");

                entity.Property(e => e.NotificationTemplate).IsRequired();

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.MasterNotification)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MasterNot__Notif__15FA39EE");
            });

            modelBuilder.Entity<MasterNotificationType>(entity =>
            {
                entity.HasKey(e => e.NotificationTypeId)
                    .HasName("PK__MasterNo__299002C1471C9C89");

                entity.Property(e => e.NotificationApi)
                    .HasColumnName("NotificationAPI")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationCategory)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK__MasterPl__755C22B7B5BC74AD");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PlanAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PlanDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PlanDuration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlanFor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlanName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__MasterRo__8AFACE1A8AC6F339");

                entity.Property(e => e.ClaimType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterSessionType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__MasterSe__516F03B5C7D5AB30");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterSpeciality>(entity =>
            {
                entity.HasKey(e => e.SpecialityId)
                    .HasName("PK__MasterSp__67ED609B642B0494");

                entity.Property(e => e.SpecialityId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SpecialityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__MasterSt__C3BA3B3AA5864607");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MasterState)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__MasterSta__Count__5D2BD0E6");
            });

            modelBuilder.Entity<MasterTag>(entity =>
            {
                entity.HasKey(e => e.TagId)
                    .HasName("PK__MasterTa__657CF9AC397B063A");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterTimezone>(entity =>
            {
                entity.HasKey(e => e.TimezoneId);

                entity.Property(e => e.TimeZoneName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TimezoneText).HasMaxLength(50);

                entity.Property(e => e.TimezoneValue).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterTopic>(entity =>
            {
                entity.HasKey(e => e.TopicId)
                    .HasName("PK__MasterTo__022E0F5D1E7149BC");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TopicImage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterUserLogActivity>(entity =>
            {
                entity.HasKey(e => e.LogActivityId)
                    .HasName("PK__MasterUs__029BCDD4D25FAD1F");

                entity.Property(e => e.LogActivityId).ValueGeneratedNever();

                entity.Property(e => e.LogActivityName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterVideos>(entity =>
            {
                entity.HasKey(e => e.VideosId)
                    .HasName("PK__MasterVi__D8294F51D54AB0E6");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsDefault)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.VideosFileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VideosLength)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.VideosName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Representative>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepresentativeCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstResponder).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NumberOfSeats).HasDefaultValueSql("((1))");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.PublishTime).HasColumnType("datetime");

                entity.Property(e => e.SeatPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.SessionDate).HasColumnType("datetime");

                entity.Property(e => e.SessionDescription).IsUnicode(false);

                entity.Property(e => e.SessionEnd).HasColumnType("datetime");

                entity.Property(e => e.SessionShotDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SessionStart).HasColumnType("datetime");

                entity.Property(e => e.SessionStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.SessionTime).HasColumnType("datetime");

                entity.Property(e => e.SessionTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimezoneId).HasDefaultValueSql("((1))");

                entity.Property(e => e.VideosId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.SessionTypeNavigation)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.SessionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Session__68687968");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__TopicId__695C9DA1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__UserId__6A50C1DA");
            });

            modelBuilder.Entity<SessionAttendee>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionAttendee)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionAt__Sessi__6B44E613");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionAttendee)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__SessionAt__UserI__6C390A4C");
            });

            modelBuilder.Entity<SessionBooking>(entity =>
            {
                entity.Property(e => e.AllowNotification).HasDefaultValueSql("((0))");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.CancelDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionBooking)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionBo__Sessi__6D2D2E85");

                entity.HasOne(d => d.SessionTypeNavigation)
                    .WithMany(p => p.SessionBooking)
                    .HasForeignKey(d => d.SessionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionBo__Sessi__6E2152BE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionBooking)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionBo__UserI__6F1576F7");
            });

            modelBuilder.Entity<SessionDocument>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionDocument)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionDo__Sessi__70099B30");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionDocument)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionDo__UserI__70FDBF69");
            });

            modelBuilder.Entity<SessionNotes>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.NotesDates).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionNotes)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionNo__Sessi__71F1E3A2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionNotes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionNo__UserI__72E607DB");
            });

            modelBuilder.Entity<SessionPayment>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentMethodDetail1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethodDetail2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentNote)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SessionPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.SessionPaymentGuid).HasColumnName("SessionPaymentGUID");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionPayment)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionPa__Sessi__73DA2C14");
            });

            modelBuilder.Entity<SessionRecording>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecordingDate).HasColumnType("datetime");

                entity.Property(e => e.RecordingName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordingTime).HasColumnType("datetime");

                entity.Property(e => e.SessionRecordingGuid).HasColumnName("SessionRecordingGUID");

                entity.HasOne(d => d.Practitioner)
                    .WithMany(p => p.SessionRecording)
                    .HasForeignKey(d => d.PractitionerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionRe__Pract__74CE504D");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionRecording)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionRe__Sessi__75C27486");
            });

            modelBuilder.Entity<SessionTag>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionTag)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionTa__Sessi__76B698BF");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.SessionTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionTa__TagId__77AABCF8");
            });

            modelBuilder.Entity<SessionVideos>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.HasOne(d => d.Videos)
                    .WithMany(p => p.SessionVideos)
                    .HasForeignKey(d => d.VideosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionVideos_MasterVideos");
            });

            modelBuilder.Entity<TestCompany>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("testCompany");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TestEmployees>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("testEmployees");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserBillingInformation>(entity =>
            {
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.UserBillingInformation)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK__UserBilli__Count__789EE131");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.UserBillingInformation)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("FK__UserBilli__State__7993056A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBillingInformation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserBilli__UserI__7A8729A3");
            });

            modelBuilder.Entity<UserBlocked>(entity =>
            {
                entity.Property(e => e.BlockedDate).HasColumnType("datetime");

                entity.Property(e => e.BlockedUserId).HasColumnName("BlockedUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBlocked)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserBlock__UserI__7B7B4DDC");
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserCompa__Compa__7C6F7215");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserCompa__UserI__7D63964E");
            });

            modelBuilder.Entity<UserCreditCard>(entity =>
            {
                entity.Property(e => e.UserCreditCardId).HasColumnName("UserCreditCardID");

                entity.Property(e => e.CardId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TokenId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserCreditCardGuid).HasColumnName("UserCreditCardGUid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCreditCard)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserCredi__UserI__7E57BA87");
            });

            modelBuilder.Entity<UserEmailLogging>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailBody)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EmailReceiver)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailSender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailSentDate).HasColumnType("datetime");

                entity.Property(e => e.EmailSubject)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmailTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.EmailTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserEmail__Email__7F4BDEC0");
            });

            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FollowerDate).HasColumnType("datetime");

                entity.Property(e => e.FollowerUserId).HasColumnName("FollowerUserID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFollower)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserFollo__UserI__004002F9");
            });

            modelBuilder.Entity<UserInterest>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.UserInterest)
                    .HasForeignKey(d => d.InterestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserInter__Inter__01342732");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInterest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserInter__UserI__02284B6B");
            });

            modelBuilder.Entity<UserInvites>(entity =>
            {
                entity.HasKey(e => e.UserInviteGuid);

                entity.Property(e => e.UserInviteGuid).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailBody).IsUnicode(false);

                entity.Property(e => e.EmailSender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailSubject)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.InviteAcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.InviteSentDate).HasColumnType("datetime");

                entity.Property(e => e.InviteSentStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.InviteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserInvites)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__UserInvit__Compa__031C6FA4");
            });

            modelBuilder.Entity<UserLanguage>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.UserLanguage)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLangu__Langu__041093DD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLanguage)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLangu__UserI__0504B816");
            });

            modelBuilder.Entity<UserLogActivity>(entity =>
            {
                entity.HasKey(e => e.UserLogActivityGuid)
                    .HasName("PK__UserLogA__31975D7D807DE7CC");

                entity.Property(e => e.UserLogActivityGuid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ipinformation)
                    .HasColumnName("IPInformation")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.LogActivity)
                    .WithMany(p => p.UserLogActivity)
                    .HasForeignKey(d => d.LogActivityId)
                    .HasConstraintName("FK__UserLogAc__LogAc__05F8DC4F");
            });

            modelBuilder.Entity<UserMembership>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRecurring)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MembershipEndDate).HasColumnType("datetime");

                entity.Property(e => e.MembershipStartDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.UserMembership)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__UserMembe__PlanI__06ED0088");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMembership)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserMembe__UserI__07E124C1");
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserNotificationSentDate).HasColumnType("datetime");

                entity.Property(e => e.UserNotificationText)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserNotif__Notif__16EE5E27");
            });

            modelBuilder.Entity<UserPasswordRequest>(entity =>
            {
                entity.HasKey(e => e.PasswordRequestId)
                    .HasName("PK__UserPass__CA31AB7AE2E77371");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordRequestDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordRequestLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPasswordRequest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserPassw__UserI__08D548FA");
            });

            modelBuilder.Entity<UserPractitoner>(entity =>
            {
                entity.HasKey(e => e.UserBlockedId)
                    .HasName("PK__UserPrac__093E807F65D69761");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PractitoneFollowerDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPractitoner)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserPract__UserI__09C96D33");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK__UserProf__290C88E4631B62B0");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.BriefBio)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Education)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FaceBookProfile)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InstagramProfile)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Language)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LinkedInProfile)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PractitionerSince).HasColumnType("datetime");

                entity.Property(e => e.ProfilePhoto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PublicProfile).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qualification)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserTypeText)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__UserProfi__Depar__0ABD916C");

                entity.HasOne(d => d.Timezone)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.TimezoneId)
                    .HasConstraintName("FK__UserProfi__Timez__1E8F7FEF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProfi__UserI__0BB1B5A5");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__RoleId__0CA5D9DE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__UserId__0D99FE17");
            });

            modelBuilder.Entity<UserSpeciality>(entity =>
            {
                entity.HasKey(e => e.UserSpeciality1)
                    .HasName("PK__UserSpec__EBE0821671BB8FE6");

                entity.Property(e => e.UserSpeciality1).HasColumnName("UserSpeciality");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.UserSpeciality)
                    .HasForeignKey(d => d.SpecialityId)
                    .HasConstraintName("FK__UserSpeci__Speci__0E8E2250");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSpeciality)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserSpeci__UserI__0F824689");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4CDB56BD49");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordExpire).HasColumnType("datetime");

                entity.Property(e => e.UnsuccessfulAttempt).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserName).HasColumnName("userName");

                entity.Property(e => e.UserPassword).HasColumnName("userPassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
