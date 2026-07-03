using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackFinder.Models.CommunityModels;
using TrackFinder.Models.UserModels;

namespace TrackFinder.ViewModels.Community;

public class CommunityFeedViewModel
{
    public string ActiveFilter { get; set; } = "all";
    public Guid? SelectedCommunityId { get; set; }
    public Guid? CurrentUserId { get; set; }
    public Guid? CurrentStudentId { get; set; }
    public PostFormViewModel NewPost { get; set; } = new();
    public IReadOnlyList<PostCardViewModel> Posts { get; set; } = [];
    public IReadOnlyList<CommunitySummaryViewModel> Communities { get; set; } = [];
    public IReadOnlyList<TopicSummaryViewModel> TrendingTopics { get; set; } = [];
    public IReadOnlyList<SelectListItem> CommunityOptions { get; set; } = [];
}

public class PostFormViewModel
{
    public Guid? Id { get; set; }

    [Required]
    public Guid GroupId { get; set; }

    [Required]
    [Display(Name = "Post")]
    public string Content { get; set; } = string.Empty;

    [Display(Name = "Image URL")]
    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public int PostPriority { get; set; }
}

public class CommentFormViewModel
{
    [Required]
    public Guid PostId { get; set; }

    public Guid? ParentCommentId { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;
}

public class PostCardViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public PostStatus Status { get; set; }
    public int ReportsCount { get; set; }
    public int PostPriority { get; set; }
    public User Author { get; set; } = null!;
    public TrackFinder.Models.CommunityModels.Community Community { get; set; } = null!;
    public IReadOnlyList<CommentPreviewViewModel> Comments { get; set; } = [];
    public IReadOnlyList<string> Tags { get; set; } = [];
    public bool CanManage { get; set; }
}

public class CommentPreviewViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public User? User { get; set; }
    public bool CanManage { get; set; }
}

public class CommunitySummaryViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MembersCount { get; set; }
    public int OnlineCount { get; set; }
    public bool IsJoined { get; set; }
}

public class TopicSummaryViewModel
{
    public string Name { get; set; } = string.Empty;
    public int PostsCount { get; set; }
}
