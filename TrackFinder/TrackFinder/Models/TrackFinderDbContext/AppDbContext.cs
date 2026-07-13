using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Models.AchievementModels;
using TrackFinder.Models.AssessmentModels;
using TrackFinder.Models.CommunityModels;
using TrackFinder.Models.ExamsAndQuizesModels;
using TrackFinder.Models.OrdersAndPayments;
using TrackFinder.Models.TeachingModels;
using TrackFinder.Models.UserModels;

/*
 *  HOW TO ADD A NEW MODEL (for the next team member):
 *  ────────────────────────────────────────────────────────────────────
 *  1. Create your model class in the appropriate Models sub-folder.
 *  2. Add a DbSet<YourModel> property in the #region DbSets block below.
 *  3. Add a private static void Configure<Module>(...) call inside
 *     OnModelCreating, then write the entity configuration there.
 *  4. Run:
 *       Add-Migration <MigrationName>
 *       Update-Database
 *     → EF Core will only create the NEW table; existing tables are safe.
 *
 *  KNOWN TYPE MISMATCHES IN CURRENT MODELS (must be fixed before migration):
 *  ────────────────────────────────────────────────────────────────────
 *  [!] Student.UserId      → string?  but User.Id is Guid
 *  [!] Instructor.UserId   → int      but User.Id is Guid
 *  [!] UserBadge.UserId    → int      but User.Id is Guid
 *  [!] Comment.UserId      → int?     but User.Id is Guid
 *  [!] StudentCertificate.StudentId → Guid  but Student.UserId is string?
 *  [!] PurchasedItem.InstructorId   → Guid  but Instructor.UserId is int
 *  [!] ExamAttempt references StudentAnswer which has no model file yet
 *  [!] Course has no FK column to TrackStack (TrackStack.Courses nav exists
 *      but Course has no TrackStackId property — add it to Course.cs)
 * ─────────────────────────────────────────────────────────────────── *
 */

namespace TrackFinder.Models.TrackFinderDbContext
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



        // ════════════════════════════════════════════════════════════
        //  DbSets  —  one per entity (= one database table)
        //  Add your new DbSet<T> here when you create a new model.
        // ════════════════════════════════════════════════════════════

        #region DbSets

        // ── User Module ──────────────────────────────────────────────
        public virtual DbSet<User>         Users       { get; set; }
        public virtual DbSet<Student>      Students    { get; set; }
        public virtual DbSet<Instructor>   Instructors { get; set; }

        // ── Achievement Module ───────────────────────────────────────
        public virtual DbSet<Badges>             Badges             { get; set; }
        public virtual DbSet<Certificate>        Certificates       { get; set; }
        public virtual DbSet<StudentCertificate> StudentCertificates { get; set; }
        public virtual DbSet<UserBadge>          UserBadges         { get; set; }

        // ── Assessment / Track Module ────────────────────────────────
        public virtual DbSet<Track>                  Tracks                  { get; set; }
        public virtual DbSet<TrackStack>             TrackStacks             { get; set; }
        public virtual DbSet<TrackSkills>            TrackSkills             { get; set; }
        public virtual DbSet<Tools>                  Tools                   { get; set; }
        public virtual DbSet<StackTools>             StackTools              { get; set; }
        public virtual DbSet<GainedSkill>            GainedSkills            { get; set; }
        public virtual DbSet<AssessmentResult>       AssessmentResults       { get; set; }
        public virtual DbSet<AssessmentResultTracks> AssessmentResultTracks  { get; set; }

        // ── Community Module ─────────────────────────────────────────
        public virtual DbSet<Community>    Communities  { get; set; }
        public virtual DbSet<Post>         Posts        { get; set; }
        public virtual DbSet<Comment>      Comments     { get; set; }
        public virtual DbSet<PostReport>   PostReports  { get; set; }
        public virtual DbSet<JoinedMembers> JoinedMembers { get; set; }

        // ── Teaching / Course Module ─────────────────────────────────
        public virtual DbSet<Course>      Courses      { get; set; }
        public virtual DbSet<CourseSkill> CourseSkills { get; set; }
        public virtual DbSet<Lesson>      Lessons      { get; set; }
        public virtual DbSet<Material>    Materials    { get; set; }
        public virtual DbSet<Enrollment>  Enrollments  { get; set; }

        // ── Exams & Quizzes Module ───────────────────────────────────
        public virtual DbSet<Exam>          Exams          { get; set; }
        public virtual DbSet<ExamAttempt>   ExamAttempts   { get; set; }
        public virtual DbSet<Question>      Questions      { get; set; }
        public virtual DbSet<Option>        Options        { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

        // ── Orders & Payments Module ─────────────────────────────────
        public virtual DbSet<PurchasedItem> PurchasedItems { get; set; }

        #endregion

        // ════════════════════════════════════════════════════════════
        //  OnModelCreating  —  all Fluent API configuration lives here,
        //  split into one private method per domain module for clarity.
        // ════════════════════════════════════════════════════════════

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUserModule(modelBuilder);
            ConfigureAchievementModule(modelBuilder);
            ConfigureAssessmentModule(modelBuilder);
            ConfigureCommunityModule(modelBuilder);
            ConfigureTeachingModule(modelBuilder);
            ConfigureExamsModule(modelBuilder);
            ConfigureOrdersModule(modelBuilder);
        }

        // ════════════════════════════════════════════════════════════
        //  Module Configurations
        // ════════════════════════════════════════════════════════════

        #region User Module

        private static void ConfigureUserModule(ModelBuilder modelBuilder)
        {
            // ── User ─────────────────────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd();

                // Unique indexes (also declared via [Index] attribute on the model,
                // but we declare them here too so the Fluent API is the single source
                // of truth and attribute-based duplication is harmless).

                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(256);

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.Gender)
                      .HasMaxLength(20);

                entity.Property(u => u.Bio)
                      .HasMaxLength(500);

                entity.Property(u => u.ProfilePictureUrl)
                      .HasMaxLength(500);

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");
            });

            // ── Student ───────────────────────────────────────────────
            // TODO [TYPE MISMATCH]: Student.UserId is declared as string?
            //      but User.Id is Guid. Change Student.UserId to Guid.
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");

                // UserId is both PK and FK → User (1-to-1 shared PK pattern)
                entity.HasKey(s => s.UserId);

                entity.Property(s => s.EducationState)
                      .HasMaxLength(50);

                entity.Property(s => s.SchoolOrUnversityName)
                      .HasMaxLength(200);

                entity.Property(s => s.Major)
                      .HasMaxLength(100);

                entity.Property(s => s.Minor)
                      .HasMaxLength(100);

                entity.Property(s => s.DegreeProgram)
                      .HasMaxLength(100);

                // 1-to-1: Student → User (Student.UserId = User.Id)
                entity.HasOne(s => s.User)
                      .WithOne(u => u.Student)
                      .HasForeignKey<Student>(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Instructor ────────────────────────────────────────────
            // TODO [TYPE MISMATCH]: Instructor.UserId is int but User.Id is Guid.
            //      Change Instructor.UserId to Guid.
            // TODO [TYPE MISMATCH]: Instructor.CommunityId is int but Community.Id is Guid.
            //      Change Instructor.CommunityId to Guid.
            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructors");

                // UserId is both PK and FK → User (1-to-1 shared PK pattern)
                entity.HasKey(i => i.UserId);

                entity.Property(i => i.Title)
                      .HasMaxLength(100);

                entity.Property(i => i.GithubLink)
                      .HasMaxLength(300);

                entity.Property(i => i.LinkedInLink)
                      .HasMaxLength(300);

                // 1-to-1: Instructor → User
                entity.HasOne(i => i.User)
                      .WithOne(u => u.Instructor)
                      .HasForeignKey<Instructor>(i => i.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Many-to-1: Instructor.ModeratedCommunity ↔ Community.Moderators
                // (one community can have many moderating instructors)
                entity.HasOne(i => i.ModeratedCommunity)
                      .WithMany(c => c.Moderators)
                      .HasForeignKey(i => i.CommunityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }



        #endregion

        #region Achievement Module

        private static void ConfigureAchievementModule(ModelBuilder modelBuilder)
        {
            // ── Badges ────────────────────────────────────────────────
            modelBuilder.Entity<Badges>(entity =>
            {
                entity.ToTable("Badges");

                entity.HasKey(b => b.BadgeId);
                entity.Property(b => b.BadgeId)
                      .ValueGeneratedOnAdd();

                entity.Property(b => b.BadgeName)
                      .HasMaxLength(100);

                entity.Property(b => b.BadgeDescription)
                      .HasMaxLength(500);

                entity.Property(b => b.BadgeImageUrl)
                      .HasMaxLength(500);
            });

            // ── UserBadge  (junction: User ↔ Badges) ─────────────────
            // TODO [TYPE MISMATCH]: UserBadge.UserId is int but User.Id is Guid.
            //      Change UserBadge.UserId to Guid.
            modelBuilder.Entity<UserBadge>(entity =>
            {
                entity.ToTable("UserBadges");

                // Composite PK: (UserId, BadgeId)
                entity.HasKey(ub => new { ub.UserId, ub.BadgeId });

                // Many UserBadge rows → one User
                entity.HasOne(ub => ub.User)
                      .WithMany(u => u.UserBadges)
                      .HasForeignKey(ub => ub.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Many UserBadge rows → one Badge
                entity.HasOne(ub => ub.Badge)
                      .WithMany(b => b.UserBadges)
                      .HasForeignKey(ub => ub.BadgeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Certificate ───────────────────────────────────────────
            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("Certificates");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(c => c.Title)
                      .HasMaxLength(200);

                entity.Property(c => c.Description)
                      .HasMaxLength(1000);

                entity.Property(c => c.CertificateUrl)
                      .HasMaxLength(500);
            });

            // ── StudentCertificate  (junction: Student ↔ Certificate ↔ Course) ──
            // TODO [TYPE MISMATCH]: StudentCertificate.StudentId is Guid
            //      but Student.UserId is string?. Change Student.UserId to Guid.
            // TODO [MODEL]: Student.AchievedCertificates nav is ICollection<Certificate>
            //      but should be ICollection<StudentCertificate> to match this junction.
            modelBuilder.Entity<StudentCertificate>(entity =>
            {
                entity.ToTable("StudentCertificates");

                // CredentialsId is the natural business key (e.g. "CERT-2026-0001")
                entity.HasKey(sc => sc.CredentialsId);
                entity.Property(sc => sc.CredentialsId)
                      .HasMaxLength(50);

                // Many StudentCertificates → one Certificate
                entity.HasOne(sc => sc.Certificate)
                      .WithMany(c => c.UserCertificates)
                      .HasForeignKey(sc => sc.CertificateId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Many StudentCertificates → one Course
                entity.HasOne(sc => sc.Course)
                      .WithMany()
                      .HasForeignKey(sc => sc.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Many StudentCertificates → one Student
                entity.HasOne(sc => sc.Student)
                      .WithMany()                         // see TODO above re nav type
                      .HasForeignKey(sc => sc.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        #endregion

        #region Assessment / Track Module

        private static void ConfigureAssessmentModule(ModelBuilder modelBuilder)
        {
            // ── Track ─────────────────────────────────────────────────
            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Tracks");

                entity.HasKey(t => t.TrackId);
                entity.Property(t => t.TrackId)
                      .ValueGeneratedOnAdd();

                entity.Property(t => t.TrackName)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(t => t.TrackDescription)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(t => t.RoadMapUrl)
                      .HasMaxLength(500);
            });

            // ── TrackStack  (stack / technology group under a Track) ──
            modelBuilder.Entity<TrackStack>(entity =>
            {
                entity.ToTable("TrackStacks");

                entity.HasKey(ts => ts.Id);
                entity.Property(ts => ts.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(ts => ts.StackName)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(ts => ts.StackDescription)
                      .HasMaxLength(500);

                // Many TrackStacks → one Track
                entity.HasOne(ts => ts.RelatedTrack)
                      .WithMany(t => t.RelatedStacks)
                      .HasForeignKey(ts => ts.RelatedTrackId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(ts => ts.Courses)
                      .WithOne(c => c.TrackStack)
                      .HasForeignKey(c => c.TrackStackId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ── Tools ─────────────────────────────────────────────────
            modelBuilder.Entity<Tools>(entity =>
            {
                entity.ToTable("Tools");

                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(t => t.Description)
                      .HasMaxLength(500);
            });

            // ── StackTools  (junction: TrackStack ↔ Tools) ───────────
            modelBuilder.Entity<StackTools>(entity =>
            {
                entity.ToTable("StackTools");

                // Composite PK: (StackId, ToolId)
                entity.HasKey(st => new { st.StackId, st.ToolId });

                entity.HasOne(st => st.RelatedStack)
                      .WithMany()
                      .HasForeignKey(st => st.StackId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(st => st.RelatedTool)
                      .WithMany()
                      .HasForeignKey(st => st.ToolId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── GainedSkill ───────────────────────────────────────────
            modelBuilder.Entity<GainedSkill>(entity =>
            {
                entity.ToTable("GainedSkills");

                entity.HasKey(gs => gs.Id);
                entity.Property(gs => gs.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(gs => gs.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(gs => gs.Description)
                      .HasMaxLength(500);
            });

            // ── TrackSkills  (junction: Track ↔ GainedSkill) ─────────
            modelBuilder.Entity<TrackSkills>(entity =>
            {
                entity.ToTable("TrackSkills");

                // Composite PK: (TrackId, SkillsId)
                entity.HasKey(tsk => new { tsk.TrackId, tsk.SkillsId });

                entity.HasOne(tsk => tsk.RelatedTrack)
                      .WithMany()
                      .HasForeignKey(tsk => tsk.TrackId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tsk => tsk.GainedSkill)
                      .WithMany()
                      .HasForeignKey(tsk => tsk.SkillsId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── AssessmentResult ──────────────────────────────────────
            modelBuilder.Entity<AssessmentResult>(entity =>
            {
                entity.ToTable("AssessmentResults");

                entity.HasKey(ar => ar.AssessmentResultId);
                entity.Property(ar => ar.AssessmentResultId)
                      .ValueGeneratedOnAdd();

                entity.Property(ar => ar.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Many AssessmentResults → one Student (FK = UserId)
                entity.HasOne(ar => ar.Student)
                      .WithMany(s => s.AssessmentResults)
                      .HasForeignKey(ar => ar.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // The AssessmentResult.RecommendedTracks nav (ICollection<Track>)
                // is handled via the AssessmentResultTracks junction below.
                // Do NOT configure a direct many-to-many here or EF will create a shadow table.
            });

            // ── AssessmentResultTracks  (junction: AssessmentResult ↔ Track) ──
            modelBuilder.Entity<AssessmentResultTracks>(entity =>
            {
                entity.ToTable("AssessmentResultTracks");

                // Composite PK: (AssessmentResultId, TrackId)
                entity.HasKey(art => new { art.AssessmentResultId, art.TrackId });

                entity.HasOne(art => art.AssessmentResult)
                      .WithMany()
                      .HasForeignKey(art => art.AssessmentResultId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(art => art.Track)
                      .WithMany()
                      .HasForeignKey(art => art.TrackId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        #endregion

        #region Community Module

        private static void ConfigureCommunityModule(ModelBuilder modelBuilder)
        {
            // ── Community ─────────────────────────────────────────────
            modelBuilder.Entity<Community>(entity =>
            {
                entity.ToTable("Communities");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                // 1-to-1: Community has one Admin Instructor
                // The FK (AdminId) lives on the Community side.
                entity.HasOne(c => c.Admin)
                      .WithOne(i => i.AdminstratedCommunity)
                      .HasForeignKey<Community>(c => c.AdminId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 1-to-Many: Community → Posts
                entity.HasMany(c => c.Posts)
                      .WithOne(p => p.Community)
                      .HasForeignKey(p => p.GroupId)
                      .OnDelete(DeleteBehavior.Cascade);

                // 1-to-Many: Community → JoinedMembers
                entity.HasMany(c => c.JoinedMembers)
                      .WithOne(jm => jm.Community)
                      .HasForeignKey(jm => jm.CommunityId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Post ──────────────────────────────────────────────────
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts");

                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(p => p.Content)
                      .IsRequired();

                entity.Property(p => p.ImageUrl)
                      .HasMaxLength(500);

                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Store enum as int column
                entity.Property(p => p.Status)
                      .HasConversion<int>();

                // Many Posts → one User (Author)
                entity.HasOne(p => p.Author)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 1-to-Many: Post → Comments
                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Comment ───────────────────────────────────────────────
            // TODO [TYPE MISMATCH]: Comment.UserId is int? but User.Id is Guid.
            //      Change Comment.UserId to Guid?.
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(c => c.Content)
                      .IsRequired();

                entity.Property(c => c.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Many Comments → one User
                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Self-referencing tree: parent comment → child replies
                entity.HasOne(c => c.ParentComment)
                      .WithMany(c => c.Replies)
                      .HasForeignKey(c => c.ParentCommentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── PostReport ────────────────────────────────────────────
            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.ToTable("PostReports");

                entity.HasKey(pr => pr.Id);
                entity.Property(pr => pr.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(pr => pr.Reason)
                      .HasConversion<int>();

                entity.Property(pr => pr.Description)
                      .HasMaxLength(1000);

                entity.Property(pr => pr.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Many PostReports → one Post
                entity.HasOne(pr => pr.Post)
                      .WithMany()
                      .HasForeignKey(pr => pr.PostId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Many PostReports → one User (Reporter)
                entity.HasOne(pr => pr.Reporter)
                      .WithMany(u => u.PostReports)
                      .HasForeignKey(pr => pr.ReporterId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── JoinedMembers  (junction: Student ↔ Community) ───────
            // TODO [TYPE MISMATCH]: JoinedMembers.MemberId is int but Student.UserId
            //      is string?. Align them both to Guid once Student is fixed.
            modelBuilder.Entity<JoinedMembers>(entity =>
            {
                entity.ToTable("JoinedMembers");

                // Composite PK: (MemberId, CommunityId)
                entity.HasKey(jm => new { jm.MemberId, jm.CommunityId });

                entity.HasOne(jm => jm.Member)
                      .WithMany(s => s.JoinedCommunities)
                      .HasForeignKey(jm => jm.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Community side is configured above via Community.HasMany(JoinedMembers)
            });
        }

        #endregion

        #region Teaching / Course Module

        private static void ConfigureTeachingModule(ModelBuilder modelBuilder)
        {
            // ── Course ────────────────────────────────────────────────
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(c => c.Description)
                      .HasMaxLength(2000);

                entity.Property(c => c.ImageUrl)
                      .HasMaxLength(500);

                // Store enums as int columns
                entity.Property(c => c.Language)
                      .HasConversion<int>();

                entity.Property(c => c.Level)
                      .HasConversion<int>();

                // CourseDuration is a struct owned value object:
                // its two fields are stored as columns in the Courses table.
                entity.OwnsOne(c => c.Duration, d =>
                {
                    d.Property(x => x.Value)
                     .HasColumnName("DurationValue");

                    d.Property(x => x.DurationIn)
                     .HasColumnName("DurationUnit")
                     .HasConversion<int>();
                });

                // Many Courses → one Instructor
                entity.HasOne(c => c.Instructor)
                      .WithMany(i => i.CreatedCourses)
                      .HasForeignKey(c => c.InstructorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── CourseSkill  (junction: Course ↔ GainedSkill) ────────
            modelBuilder.Entity<CourseSkill>(entity =>
            {
                entity.ToTable("CourseSkills");

                // Composite PK: (CourseId, GainedSkillId)
                entity.HasKey(cs => new { cs.CourseId, cs.GainedSkillId });

                entity.HasOne(cs => cs.Course)
                      .WithMany(c => c.CourseSkills)
                      .HasForeignKey(cs => cs.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cs => cs.GainedSkill)
                      .WithMany()
                      .HasForeignKey(cs => cs.GainedSkillId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Lesson ────────────────────────────────────────────────
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lessons");

                entity.HasKey(l => l.Id);
                entity.Property(l => l.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(l => l.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(l => l.Description)
                      .HasMaxLength(1000);

                // LessonDuration is a struct owned value object:
                // stored as DurationMinutes + DurationSeconds columns in Lessons table.
                entity.OwnsOne(l => l.Duration, d =>
                {
                    d.Property(x => x.Minutes)
                     .HasColumnName("DurationMinutes");

                    d.Property(x => x.Seconds)
                     .HasColumnName("DurationSeconds");
                });

                // Many Lessons → one Course (nullable: a Lesson may be standalone)
                entity.HasOne(l => l.Course)
                      .WithMany(c => c.Lessons)
                      .HasForeignKey(l => l.CourseId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ── Material ──────────────────────────────────────────────
            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Materials");

                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(m => m.FileName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(m => m.FileUrl)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(m => m.ContentType)
                      .IsRequired()
                      .HasMaxLength(100);

                // Material belongs to a Lesson
                entity.HasOne(m => m.Lesson)
                      .WithMany(l => l.Materials)
                      .HasForeignKey(m => m.LessonId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Material also belongs to a Course (course-level attachment)
                entity.HasOne(m => m.Course)
                      .WithMany(c => c.Materials)
                      .HasForeignKey(m => m.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Enrollment  (junction: User/Student ↔ Course) ─────────
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollments");

                // Composite PK: (CourseId, UserId)
                entity.HasKey(e => new { e.CourseId, e.UserId });

                entity.Property(e => e.Status)
                      .HasConversion<int>();

                entity.Property(e => e.EnrollmentDate)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Many Enrollments → one Course
                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Many Enrollments → one User (the enrolled student)
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        #endregion

        #region Exams & Quizzes Module

        private static void ConfigureExamsModule(ModelBuilder modelBuilder)
        {
            // ── Exam ──────────────────────────────────────────────────
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exams");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                      .HasMaxLength(1000);

                // Many Exams → one Lesson
                entity.HasOne(e => e.Lesson)
                      .WithMany(l => l.Exams)
                      .HasForeignKey(e => e.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── ExamAttempt ───────────────────────────────────────────
            modelBuilder.Entity<ExamAttempt>(entity =>
            {
                entity.ToTable("ExamAttempts");

                entity.HasKey(ea => ea.Id);
                entity.Property(ea => ea.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(ea => ea.AttemptDate)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Many ExamAttempts → one Exam
                entity.HasOne(ea => ea.Exam)
                      .WithMany(e => e.ExamAttempts)
                      .HasForeignKey(ea => ea.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Question ──────────────────────────────────────────────
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Questions");

                entity.HasKey(q => q.Id);
                entity.Property(q => q.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(q => q.QuestionText)
                      .IsRequired();

                entity.Property(q => q.QuestionType)
                      .HasConversion<int>();

                // Many Questions → one Exam
                entity.HasOne(q => q.Exam)
                      .WithMany(e => e.Questions)
                      .HasForeignKey(q => q.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── StudentAnswer ─────────────────────────────────────────
            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.ToTable("StudentAnswers");

                entity.HasKey(sa => sa.Id);
                entity.Property(sa => sa.Id)
                      .ValueGeneratedOnAdd();

                // Many StudentAnswers → one ExamAttempt
                entity.HasOne(sa => sa.ExamAttempt)
                      .WithMany(ea => ea.StudentAnswers)
                      .HasForeignKey(sa => sa.ExamAttemptId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Many StudentAnswers → one Question
                entity.HasOne(sa => sa.Question)
                      .WithMany()
                      .HasForeignKey(sa => sa.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Many StudentAnswers → one Option (nullable = skipped question)
                entity.HasOne(sa => sa.SelectedOption)
                      .WithMany()
                      .HasForeignKey(sa => sa.SelectedOptionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Option ────────────────────────────────────────────────
            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("Options");

                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(o => o.OptionText)
                      .IsRequired()
                      .HasMaxLength(500);

                // Many Options → one Question
                entity.HasOne(o => o.Question)
                      .WithMany(q => q.Options)
                      .HasForeignKey(o => o.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        #endregion

        #region Orders & Payments Module

        private static void ConfigureOrdersModule(ModelBuilder modelBuilder)
        {
            // ── PurchasedItem ─────────────────────────────────────────
            // NOTE: PurchasedItemId is a single int column that acts as the FK
            //       to either Course OR Lesson, discriminated by the "type" field.
            //       This is a polymorphic reference — EF cannot enforce both FKs on
            //       the same column. Consider splitting into CourseId + LessonId
            //       (both nullable) for proper relational integrity.
            // TODO [TYPE MISMATCH]: PurchasedItem.InstructorId is Guid but
            //      Instructor.UserId is int. Align to Guid after fixing Instructor.
            modelBuilder.Entity<PurchasedItem>(entity =>
            {
                entity.ToTable("PurchasedItems");

                entity.HasKey(pi => pi.PurchasingId);
                entity.Property(pi => pi.PurchasingId)
                      .ValueGeneratedOnAdd();

                // Rename the lower-case property to follow conventions
                entity.Property(pi => pi.type)
                      .HasColumnName("Type")
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(pi => pi.PurchaseDate)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Many PurchasedItems → one Student
                entity.HasOne(pi => pi.Student)
                      .WithMany()
                      .HasForeignKey(pi => pi.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Many PurchasedItems → one Instructor
                entity.HasOne(pi => pi.Instructor)
                      .WithMany()
                      .HasForeignKey(pi => pi.InstructorId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Course / Lesson FK cannot both point at PurchasedItemId.
                // Relationships left unforced here until the model is refactored.
                // Once CourseId / LessonId are added as separate nullable columns:
                // entity.HasOne(pi => pi.Course)
                //       .WithMany()
                //       .HasForeignKey(pi => pi.CourseId)
                //       .OnDelete(DeleteBehavior.Restrict);
                //
                // entity.HasOne(pi => pi.Lesson)
                //       .WithMany()
                //       .HasForeignKey(pi => pi.LessonId)
                //       .OnDelete(DeleteBehavior.Restrict);
            });
        }

        #endregion
    }
}
