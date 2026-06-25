using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;
using TrackFinderDb.Models.AchievementModels;
using TrackFinderDb.Models.CommunityModels;

/*
-- ////////////////////////////////////////////////
--                                                
-- ██╗   ██╗  ███████╗  ███████╗  ██████╗        
-- ██║   ██║  ██╔════╝  ██╔════╝  ██╔══██╗       
-- ██║   ██║  ███████╗  █████╗    ██████╔╝       
-- ██║   ██║  ╚════██║  ██╔══╝    ██╔══██╗       
-- ╚██████╔╝  ███████║  ███████╗  ██║  ██║       
--  ╚═════╝   ╚══════╝  ╚══════╝  ╚═╝  ╚═╝       
--                                                
-- ////////////////////////////////////////////////

-- *-------------------------------------------------------*
-- USER IS A BASE TABLE FOR OUR USERS [Student, Instructor]
-- *-------------------------------------------------------*

--    UserId              - Unique identifier for each user, auto-incremented
--    UserName            - Public display name / handle, must be unique across all users
--    FirstName           - User's given first name
--    LastName            - User's family / last name
--    UserBirthdate       - Date of birth, optional (nullable for privacy)
--    UserEmail           - Email address used for login and notifications, must be unique
--    UserPasswordHash    - Hashed password for authentication (never store plain text!)
--    Gender              - User gender (e.g., Male, Female)
--    Bio                 - Short self-description or about me text, optional
--    ProfilePictureUrl   - URL pointing to the user's profile picture / avatar
--    EmailVerified       - Whether the user has confirmed their email address (0 = no, 1 = yes)
--    IsBanned            - Whether the user is banned from the platform (0 = active, 1 = banned)
--    CreatedAt           - Timestamp of when the user account was created
--    Student             - Navigation property for related Student entity (if user is a student)
--    Instructor          - Navigation property for related Instructor entity (if user is an instructor)
--    UserBadges          - Navigation property for the collection of badges earned by the user (many-to-many relationship with Badges through UserBadge)
 */


namespace TrackFinderDb.Models.UserModels
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string? Gender { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool EmailVerified { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = string.Empty;

        public virtual Student? Student { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual ICollection<UserBadge>? UserBadges { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<PostReport>? PostReports { get; set; }
    }
}
