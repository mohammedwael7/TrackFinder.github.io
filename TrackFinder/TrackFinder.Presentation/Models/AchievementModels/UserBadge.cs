using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.AchievementModels
{
    public class UserBadge
    {
        public DateTime EarnedAt { get; set; }

        public Guid UserId { get; set; }
        public int BadgeId { get; set; }
        public virtual User? User { get; set; }
        public virtual Badges? Badge { get; set; }

    }
}