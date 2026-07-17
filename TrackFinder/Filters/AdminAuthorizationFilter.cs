using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Filters;

public class AdminAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly AppDbContext _context;

    public AdminAuthorizationFilter(AppDbContext context)
    {
        _context = context;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userIdString = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
            return;
        }

        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null || user.Role != UserRole.Admin)
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
}
