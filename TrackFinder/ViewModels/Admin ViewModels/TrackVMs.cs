using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TrackFinder.ViewModels.Tracks
{
    public class TrackListVM
    {
        public int Id { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public string TrackDescription { get; set; } = string.Empty;
        public string? RoadMapUrl { get; set; }
    }

    public class CreateTrackVM
    {
        [Required]
        public string TrackName { get; set; } = string.Empty;
        public string? TrackDescription { get; set; }
        public string? RoadMapUrl { get; set; }
    }

    public class EditTrackVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string TrackName { get; set; } = string.Empty;
        public string? TrackDescription { get; set; }
        public string? RoadMapUrl { get; set; }
    }
    public class TrackDetailsVM
    {
        public int Id { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public string? TrackDescription { get; set; }
        public string? RoadMapUrl { get; set; }
    }
}
