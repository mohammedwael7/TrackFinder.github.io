using System.ComponentModel.DataAnnotations;

namespace TrackFinder.Models.AchievementModels
{
	public class Badges
	{
		[Key]
		public int BadgeId { get; set; }
		public string? BadgeName { get; set; }
		public string? BadgeDescription { get; set; }
		public string? BadgeImageUrl { get; set; }

		public virtual ICollection<UserBadge>? UserBadges { get; set; }
	}
}