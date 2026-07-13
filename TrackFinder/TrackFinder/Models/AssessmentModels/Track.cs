using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrackFinder.Models.AssessmentModels;
using TrackFinder.Models.UserModels;

/*
-- ////////////////////////////////////////////////
--
-- ████████╗ ██████╗   █████╗   ██████╗ ██╗  ██╗
-- ╚══██╔══╝ ██╔══██╗ ██╔══██╗ ██╔════╝ ██║ ██╔╝ 
--    ██║    ██████╔╝ ███████║ ██║      █████╔╝  
--    ██║    ██╔══██╗ ██╔══██║ ██║      ██╔═██╗  
--    ██║    ██║  ██║ ██║  ██║ ╚██████╗ ██║  ██╗ 
--    ╚═╝    ╚═╝  ╚═╝ ╚═╝  ╚═╝  ╚═════╝ ╚═╝  ╚═╝ 
--                                               
-- ////////////////////////////////////////////////

-- *-------------------------------------------------------*
--    TRACK IS A TABLE FOR LEARNING TRACKS / CATEGORIES
-- *-------------------------------------------------------*

--    TrackId             - Unique identifier formatted as TRC-001, TRC-002, etc.
--    TrackName           - Name of the learning track, must be unique
--    TrackDescription    - Brief description of what the track covers, optional
--    TrackImageUrl       - URL to an image representing the track, optional
--    RoadMapUrl          - URL to a roadmap or curriculum for the track, optional
*/

namespace TrackFinder.Models.AssessmentModels
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        [Required]
        public string TrackName { get; set; } = string.Empty;
        [Required]
        public string TrackDescription { get; set; } = string.Empty;
        public string? RoadMapUrl { get; set; }

        public virtual ICollection<TrackStack>? RelatedStacks { get; set; }
        public virtual ICollection<AssessmentResult>? AssessmentResults { get; set; }
        public virtual ICollection<GainedSkill>? GainedSkills { get; set; }
    }
}
