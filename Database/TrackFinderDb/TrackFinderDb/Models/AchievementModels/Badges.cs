using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/*
 * ------------------------------------------------ *
 * Updated At: 2026-05-24                           |
 * Updated By: [Ahmed Ehab]                         |
 * ------------------------------------------------ *
-- ///////////////////////////////////////////////////////////////////
--                                                                  
-- ██████╗   █████╗  ██████╗   ██████╗  ███████╗ ███████╗
-- ██╔══██╗ ██╔══██╗ ██╔══██╗ ██╔════╝  ██╔════╝ ██╔════╝
-- ██████╔╝ ███████║ ██║  ██║ ██║  ███╗ █████╗   ███████╗
-- ██╔══██╗ ██╔══██║ ██║  ██║ ██║   ██║ ██╔══╝   ╚════██║
-- ██████╔╝ ██║  ██║ ██████╔╝ ╚██████╔╝ ███████╗ ███████║
-- ╚═════╝  ╚═╝  ╚═╝ ╚═════╝   ╚═════╝  ╚══════╝ ╚══════╝
--                                                                  
-- ///////////////////////////////////////////////////////////////////

-- *-------------------------------------------------------*
--   BADGES TABLE FOR ACHIEVEMENT/BADGE DEFINITIONS
-- *-------------------------------------------------------*

--    BadgeId          - Primary key, unique identifier for each badge
--    BadgeName        - Display name of the badge, optional
--    BadgeDescription - Description of what the badge represents or how to earn it, optional
--    BadgeImageUrl    - URL path to the badge's image/icon asset, optional
--    UserBadges       - Navigation property for the collection of UserBadge entries that link this badge to users who have earned it (many-to-many relationship with User through UserBadge)
*/

namespace TrackFinderDb.Models.AchievementModels
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