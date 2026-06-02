using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrackFinderDb.Models.AssesmentModels;
using TrackFinderDb.Models.TrackModels;
using TrackFinderDb.Models.UserModels;
using TrackFinderDb.Models.TeachingModels;

/*
 * ------------------------------------------------ *
 * Last Updated: 2026-05-24                         |
 * Updated By: [Ahmed Ehab]                         |
 * ------------------------------------------------ *
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

namespace TrackFinderDb.Models.TrackModels
{
    public class Track
    {
        [Key]
        public string? TrackId { get; set; }
        [Required]
        public string? TrackName { get; set; }
        [Required]
        public string? TrackDescription { get; set; }
        public string? TrackImageUrl { get; set; }
        public string? RoadMapUrl { get; set; }

        public ICollection<Stack> RelatedStacks { get; set; }
        public ICollection<AssessmentResult> AssessmentResults { get; set; }
    }
}
