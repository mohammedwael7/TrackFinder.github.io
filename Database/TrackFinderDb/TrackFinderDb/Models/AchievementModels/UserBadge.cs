using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

/*
 * ------------------------------------------------ *
 * Updated At: 2026-05-24                           |
 * Updated By: [Ahmed Ehab]                         |
 * ------------------------------------------------ *
-- //////////////////////////////////////////////////////////////////////////////////////
--                                                                  
-- ██╗   ██╗ ███████╗ ███████╗ ██████╗     ██████╗   █████╗  ██████╗   ██████╗  ███████╗
-- ██║   ██║ ██╔════╝ ██╔════╝ ██╔══██╗    ██╔══██╗ ██╔══██╗ ██╔══██╗ ██╔════╝  ██╔════╝
-- ██║   ██║ ███████╗ █████╗   ██████╔╝    ██████╔╝ ███████║ ██║  ██║ ██║  ███╗ █████╗
-- ██║   ██║ ╚════██║ ██╔══╝   ██╔══██╗    ██╔══██╗ ██╔══██║ ██║  ██║ ██║   ██║ ██╔══╝
-- ╚██████╔╝ ███████║ ███████╗ ██║  ██║    ██████╔╝ ██║  ██║ ██████╔╝ ╚██████╔╝ ███████╗
--  ╚═════╝  ╚══════╝ ╚══════╝ ╚═╝  ╚═╝    ╚═════╝  ╚═╝  ╚═╝ ╚═════╝   ╚═════╝  ╚══════╝
--                                                                  
-- //////////////////////////////////////////////////////////////////////////////////////

-- *--------------------------------------------------------------*
--   USERBADGE IS A JUNCTION TABLE LINKING USERS TO EARNED BADGES
-- *--------------------------------------------------------------*

--    UserId    - FK to User table, part of composite primary key (many-to-many relationship)
--    BadgeId   - FK to Badges table, part of composite primary key (many-to-many relationship)
--    EarnedAt  - Timestamp of when the user earned this badge
--    User      - Navigation property to the related User entity (many-to-one relationship)
--    Badge     - Navigation property to the related Badges entity (many-to-one relationship)
*/

namespace TrackFinderDb.Models.AchievementModels
{
    public class UserBadge
    {
        public int UserId { get; set; }
        public int BadgeId { get; set; }

        public virtual User? User { get; set; }
        public virtual Badges? Badge { get; set; }

        public DateTime EarnedAt { get; set; }
    }
}