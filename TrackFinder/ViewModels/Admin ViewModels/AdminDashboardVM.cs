namespace TrackFinder.ViewModels.Admin_ViewModels
{
    public class AdminDashboardVM
    {
        // Cards
        public int UsersCount { get; set; }
        public int StudentsCount { get; set; }
        public int InstructorsCount { get; set; }
        public int PendingInstructors { get; set; }

        public int CoursesCount { get; set; }
        public int TracksCount { get; set; }
        public int CommunitiesCount { get; set; }
        public int CertificatesCount { get; set; }
        public int BadgesCount { get; set; }

        public int VerifiedEmails { get; set; }
        public int BannedUsers { get; set; }


        // Chart Data

        public int StudentsPercentage { get; set; }
        public int InstructorPercentage { get; set; }
        public int AdminPercentage { get; set; }


        // Latest Users

        public List<DashboardUserVM> LatestUsers { get; set; }
            = new();


        // Pending Instructor Requests

        public List<InstructorRequestDashboardVM> InstructorRequests { get; set; }
            = new();

    }


    public class DashboardUserVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string Role { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }



    public class InstructorRequestDashboardVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string Title { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}