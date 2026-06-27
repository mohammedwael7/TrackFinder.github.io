namespace TrackFinder.Models.AchievementModels
{
	public class Certificate
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? CertificateUrl { get; set; }
		public virtual ICollection<StudentCertificate>? UserCertificates { get; set; }
	}
}
