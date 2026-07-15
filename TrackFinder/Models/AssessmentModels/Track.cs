namespace TrackFinder.Models.AssessmentModels
{
    public class Track
    {
        public int Id { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public string TrackDescription { get; set; } = string.Empty;
        public string? RoadMapUrl { get; set; }

        public virtual ICollection<TrackStack>? RelatedStacks { get; set; }
        public virtual ICollection<AssessmentResult>? AssessmentResults { get; set; }
        public virtual ICollection<GainedSkill>? GainedSkills { get; set; }
    }
}
