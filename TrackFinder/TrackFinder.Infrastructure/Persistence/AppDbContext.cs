using Microsoft.EntityFrameworkCore;
using TrackFinder.Models.AchievementModels;
using TrackFinder.Models.AssessmentModels;
using TrackFinder.Models.CommunityModels;
using TrackFinder.Models.CourseModels;
using TrackFinder.Models.OrdersAndPaymentsModels;
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

namespace TrackFinderDb.Models.TrackFinderDbContext
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		// ASP Monster Connection string

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	if (!optionsBuilder.IsConfigured)
		//	{
		//		optionsBuilder.UseSqlServer("Server=db57022.public.databaseasp.net; Database=db57022; User Id=db57022; Password=Sb8?s4@F+E2g; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
		//	}
		//}


		#region DbSets

		// ── User Module ──────────────────────────────────────────────
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Instructor> Instructors { get; set; }

		// ── Achievement Module ───────────────────────────────────────
		public virtual DbSet<Badges> Badges { get; set; }
		public virtual DbSet<Certificate> Certificates { get; set; }
		public virtual DbSet<StudentCertificate> StudentCertificates { get; set; }
		public virtual DbSet<UserBadge> UserBadges { get; set; }

		// ── Assessment / Track Module ────────────────────────────────
		public virtual DbSet<Track> Tracks { get; set; }
		public virtual DbSet<TrackStack> TrackStacks { get; set; }
		public virtual DbSet<TrackSkills> TrackSkills { get; set; }
		public virtual DbSet<Tools> Tools { get; set; }
		public virtual DbSet<StackTools> StackTools { get; set; }
		public virtual DbSet<GainedSkill> GainedSkills { get; set; }
		public virtual DbSet<AssessmentResult> AssessmentResults { get; set; }
		public virtual DbSet<AssessmentResultTracks> AssessmentResultTracks { get; set; }

		// ── Community Module ─────────────────────────────────────────
		public virtual DbSet<Community> Communities { get; set; }
		public virtual DbSet<Post> Posts { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<PostReport> PostReports { get; set; }
		public virtual DbSet<JoinedMembers> JoinedMembers { get; set; }

		// ── Teaching / Course Module ─────────────────────────────────
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<CourseSkill> CourseSkills { get; set; }
		public virtual DbSet<Lesson> Lessons { get; set; }
		public virtual DbSet<Material> Materials { get; set; }
		public virtual DbSet<Enrollment> Enrollments { get; set; }

		// ── Exams & Quizzes Module ───────────────────────────────────
		public virtual DbSet<Exam> Exams { get; set; }
		public virtual DbSet<ExamAttempt> ExamAttempts { get; set; }
		public virtual DbSet<Question> Questions { get; set; }
		public virtual DbSet<Option> Options { get; set; }
		public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

		// ── Orders & Payments Module ─────────────────────────────────
		public virtual DbSet<Purchasing> PurchasedItems { get; set; }

		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}
