using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackFinder.DTOs;
using TrackFinder.DTOs.AuthenticationDTOs;

namespace TrackFinder.Services.UserProfileServices
{
    public interface IUserProfileService
    {
        Task<DashboardViewModel> GetDashboardDataAsync(Guid userId, string role);
        Task<EditProfileDto> GetProfileForEditAsync(Guid userId, string role);
        Task<AuthResultDto> UpdateProfileAsync(Guid userId, string role, EditProfileDto dto, string webRootPath);
        Task<List<UserSearchResultDto>> SearchUsersAsync(string query, Guid currentUserId);
        Task<PublicProfileDto> GetPublicProfileAsync(Guid userId);
    }
}
