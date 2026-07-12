using TrackFinder.Models.AchievementModels;

namespace TrackFinder.Models.ViewModels
{
    public class AchievementViewModel
    {
        // الشهادات بتاعت الطالب
        public List<StudentCertificate> StudentCertificates { get; set; } = new();

        // المميز Badge  الـ (Featured)
        public StudentCertificate? FeaturedCertificate { get; set; }

        //  الموجودة في النظام  Badges كل الـ
        public List<Badges> AllBadges { get; set; } = new();

        //  اللي الطالب كسبها Badges
        public List<UserBadge> EarnedBadges { get; set; } = new();

        // الشهادة الجاية (Next Milestone)
        public Certificate? NextMilestoneCertificate { get; set; }
    }
}