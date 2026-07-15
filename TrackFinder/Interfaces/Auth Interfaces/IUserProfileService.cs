using TrackFinder.ViewModels.Auth_ViewModels;
using TrackFinder.ViewModels.Profile_ViewModels;

namespace TrackFinder.Services.UserProfileServices
{
    public interface IUserProfileService
    {
        Task<DashboardVM> GetDashboardDataAsync(Guid userId, string role);
        Task<EditProfileVM> GetProfileForEditAsync(Guid userId, string role);
        Task<AuthResultVM> UpdateProfileAsync(Guid userId, string role, EditProfileVM dto, string webRootPath);
    }
}
