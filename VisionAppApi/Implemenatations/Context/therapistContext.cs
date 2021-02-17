using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VisionAppApi.Implemenatations.Context
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
                optionsBuilder.UseSqlServer("Server=172.24.20.111;Database=therapist;uid=therapist;password=therapist;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDetails>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__AccountD__349DA5A65000FAAE");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AccountDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountDe__UserI__51EF2864");
            });

            modelBuilder.Entity<BlockUserHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK__BlockUse__4D7B4ABDBE5F4606");

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
                    .HasConstraintName("FK__Company__Company__73501C2F");

                entity.HasOne(d => d.CompanyStateNavigation)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CompanyState)
                    .HasConstraintName("FK__Company__Company__725BF7F6");

                entity.HasOne(d => d.Representative)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.RepresentativeId)
                    .HasConstraintName("FK__Company__Represe__7AF13DF7");
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
                    .HasConstraintName("FK__CompanyBi__Compa__6D9742D9");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.CompanyBillingInformation)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK__CompanyBi__Count__6BAEFA67");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.CompanyBillingInformation)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("FK__CompanyBi__State__6CA31EA0");
            });

            modelBuilder.Entity<CompanyRatePlan>(entity =>
            {
                entity.HasKey(e => e.CompanyRateTableId)
                    .HasName("PK__CompanyR__56032F63129E9740");

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
                    .HasConstraintName("FK__CompanyRa__Compa__7C4F7684");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.CompanyRatePlan)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__CompanyRa__PlanI__7BE56230");
            });

            modelBuilder.Entity<CompanyUpload>(entity =>
            {
                entity.HasKey(e => e.UploadId)
                    .HasName("PK__CompanyU__6D16C84DE26518C9");

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
                    .HasName("PK__CompanyU__5E54864860DCDB12");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyUserMember)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyUs__Compa__50FB042B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CompanyUserMember)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyUs__UserI__5006DFF2");
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
                    .HasConstraintName("FK__Invoice__UserId__7EF6D905");
            });

            modelBuilder.Entity<MasterCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__MasterCo__10D1609FB60E3585");

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
                    .HasName("PK__MasterDe__B2079BED162AA1C6");

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
                    .HasName("PK__MasterEm__BC0A3875E926B61F");

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
                    .HasName("PK__MasterIn__20832C67E1AB2E7F");

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
                    .HasName("PK__MasterLa__B93855ABBB77BD79");

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
                    .HasName("PK__MasterNo__20CF2E1266D06007");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NotificationIntervalType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationTemplate).IsRequired();

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.MasterNotification)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MasterNot__Notif__731B1205");
            });

            modelBuilder.Entity<MasterNotificationType>(entity =>
            {
                entity.HasKey(e => e.NotificationTypeId)
                    .HasName("PK__MasterNo__299002C1BBAD10A3");

                entity.Property(e => e.NotificationTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK__MasterPl__755C22B7E768A5D4");

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
                    .HasName("PK__MasterRo__8AFACE1AC93689A3");

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
                    .HasName("PK__MasterSe__516F03B5B245AC0D");

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
                    .HasName("PK__MasterSp__67ED609BDB27B419");

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
                    .HasName("PK__MasterSt__C3BA3B3A0F80234C");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MasterState)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__MasterSta__Count__6166761E");
            });

            modelBuilder.Entity<MasterTag>(entity =>
            {
                entity.HasKey(e => e.TagId)
                    .HasName("PK__MasterTa__657CF9ACE284CCEC");

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

                entity.Property(e => e.TimezoneId).ValueGeneratedNever();

                entity.Property(e => e.TimeZoneName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TimezoneText).HasMaxLength(50);

                entity.Property(e => e.TimezoneValue).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterTopic>(entity =>
            {
                entity.HasKey(e => e.TopicId)
                    .HasName("PK__MasterTo__022E0F5D6164927A");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TopicImage)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NA.jpg')");

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterUserLogActivity>(entity =>
            {
                entity.HasKey(e => e.LogActivityId)
                    .HasName("PK__MasterUs__029BCDD40364D778");

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

                entity.Property(e => e.IsAccepted).HasDefaultValueSql("((0))");

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

                entity.Property(e => e.SessionShotDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SessionStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.SessionTime).HasColumnType("datetime");

                entity.Property(e => e.SessionTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SessionTypeNavigation)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.SessionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Session__51300E55");

                entity.HasOne(d => d.Timezone)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.TimezoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Timezon__65C116E7");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__TopicId__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__UserId__693CA210");
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
                    .HasConstraintName("FK__SessionAt__Sessi__7F2BE32F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionAttendee)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__SessionAt__UserI__7DCDAAA2");
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
                    .HasConstraintName("FK__SessionBo__Sessi__73852659");

                entity.HasOne(d => d.SessionTypeNavigation)
                    .WithMany(p => p.SessionBooking)
                    .HasForeignKey(d => d.SessionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionBo__Sessi__756D6ECB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionBooking)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionBo__UserI__7EC1CEDB");
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
                    .HasConstraintName("FK__SessionDo__Sessi__06CD04F7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionDocument)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionDo__UserI__7FB5F314");
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
                    .HasConstraintName("FK__SessionNo__Sessi__02FC7413");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionNotes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionNo__UserI__00AA174D");
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
                    .HasConstraintName("FK__SessionPa__Sessi__73BA3083");
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
                    .HasConstraintName("FK__SessionRe__Pract__0B91BA14");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionRecording)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionRe__Sessi__0A9D95DB");
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
                    .HasConstraintName("FK__SessionTa__Sessi__70DDC3D8");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.SessionTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionTa__TagId__6FE99F9F");
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
                    .HasConstraintName("FK__UserBilli__Count__03BB8E22");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.UserBillingInformation)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("FK__UserBilli__State__02C769E9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBillingInformation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserBilli__UserI__01D345B0");
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
                    .HasConstraintName("FK__UserBlock__UserI__55BFB948");
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserCompa__Compa__17C286CF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserCompa__UserI__16CE6296");
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
                    .HasConstraintName("FK__UserCredi__UserI__440B1D61");
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
                    .HasConstraintName("FK__UserEmail__IsDel__2C88998B");
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
                    .HasConstraintName("FK__UserFollo__UserI__5AEE82B9");
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
                    .HasConstraintName("FK__UserInter__Inter__5165187F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInterest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserInter__UserI__5070F446");
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
                    .HasConstraintName("FK__UserInvit__Compa__1881A0DE");
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
                    .HasConstraintName("FK__UserLangu__Langu__5535A963");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLanguage)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLangu__UserI__5441852A");
            });

            modelBuilder.Entity<UserLogActivity>(entity =>
            {
                entity.HasKey(e => e.UserLogActivityGuid)
                    .HasName("PK__UserLogA__31975D7D8D7C1297");

                entity.Property(e => e.UserLogActivityGuid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ipinformation)
                    .HasColumnName("IPInformation")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.LogActivity)
                    .WithMany(p => p.UserLogActivity)
                    .HasForeignKey(d => d.LogActivityId)
                    .HasConstraintName("FK__UserLogAc__LogAc__65F62111");
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
                    .HasConstraintName("FK__UserMembe__PlanI__019E3B86");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMembership)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserMembe__UserI__7B264821");
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserNotificationSentDate).HasColumnType("datetime");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserNotif__Notif__75F77EB0");
            });

            modelBuilder.Entity<UserPasswordRequest>(entity =>
            {
                entity.HasKey(e => e.PasswordRequestId)
                    .HasName("PK__Password__CA31AB7A2AFE30EE");

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
                    .HasConstraintName("FK__PasswordR__UserI__1E6F845E");
            });

            modelBuilder.Entity<UserPractitoner>(entity =>
            {
                entity.HasKey(e => e.UserBlockedId)
                    .HasName("PK__UserPrac__093E807F16BDE825");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PractitoneFollowerDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPractitoner)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserPract__UserI__5DCAEF64");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK__UserProf__290C88E419213ACD");

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
                    .HasConstraintName("FK__UserProfi__Depar__5F492382");

                entity.HasOne(d => d.Timezone)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.TimezoneId)
                    .HasConstraintName("FK__UserProfi__Timez__7ABC33CD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProfi__UserI__412EB0B6");
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
                    .HasConstraintName("FK__UserRole__RoleId__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__UserId__3D5E1FD2");
            });

            modelBuilder.Entity<UserSpeciality>(entity =>
            {
                entity.HasKey(e => e.UserSpeciality1)
                    .HasName("PK__UserSpec__EBE082160AB2B4B7");

                entity.Property(e => e.UserSpeciality1).HasColumnName("UserSpeciality");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.UserSpeciality)
                    .HasForeignKey(d => d.SpecialityId)
                    .HasConstraintName("FK__UserSpeci__Speci__038683F8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSpeciality)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserSpeci__UserI__02925FBF");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C2DDF1E5A");

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
