using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.AssessmentModels
{
    public class AssessmentResult
    {
        public int AssessmentResultId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public virtual Student Student { get; set; } = null!;
        public virtual ICollection<Track> RecommendedTracks { get; set; } = new List<Track>();
    }
   
}
